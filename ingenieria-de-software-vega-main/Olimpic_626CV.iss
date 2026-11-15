; =====================================================================
;  Instalador de Olimpic - Trabajo de Diploma (Camila Vega Ronchi - 626CV)
;  Herramienta: Inno Setup 6 (https://jrsoftware.org/isdl.php)
;
;  Requisitos de la sala que cumple:
;   - Se instala SIN permisos de administrador, en la carpeta del usuario
;     (%LOCALAPPDATA%\Programs\Olimpic), donde siempre puede escribir.
;   - Pide la instancia de SQL Server y la guarda en el archivo de
;     configuración (editable después con el Bloc de notas).
;   - La base de datos la crea el propio programa al primer inicio
;     (script embebido en DAL.dll), con autenticación de Windows.
;
;  Este archivo va en la carpeta de la solución (al lado del .sln).
;  Antes de compilarlo: Visual Studio en modo Release -> Compilar solución.
; =====================================================================

#define MiAplicacion   "Olimpic"
#define MiVersion      "2.0"
#define MiEditor       "Camila Vega Ronchi - UAI"
#define MiEjecutable   "ingenieria_software_santander_vega.exe"
#define CarpetaRelease "ingenieria_software_santander_vega\bin\Release"

[Setup]
AppId={{6A3F2C1E-626C-4E0A-9C1B-0B1E2D3F626C}
AppName={#MiAplicacion}
AppVersion={#MiVersion}
AppPublisher={#MiEditor}
AppVerName={#MiAplicacion} {#MiVersion}
; Instalación por usuario: no pide administrador
PrivilegesRequired=lowest
DefaultDirName={autopf}\{#MiAplicacion}
DefaultGroupName={#MiAplicacion}
DisableProgramGroupPage=yes
OutputDir=Instalador
OutputBaseFilename=Olimpic_Setup_626CV
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
UninstallDisplayIcon={app}\{#MiEjecutable}
; El programa no necesita reiniciar Windows
RestartIfNeededByRun=no
CloseApplications=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "escritorio"; Description: "Crear un acceso directo en el escritorio"; GroupDescription: "Accesos directos:"

[Files]
; Todo lo que genera la compilación en Release (exe, DLL de las capas, librerías y .config)
Source: "{#CarpetaRelease}\*"; DestDir: "{app}"; Excludes: "*.pdb,*.vshost.*,*.xml"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MiAplicacion}"; Filename: "{app}\{#MiEjecutable}"
Name: "{group}\Configuración de conexión (Bloc de notas)"; Filename: "notepad.exe"; Parameters: """{app}\{#MiEjecutable}.config"""
Name: "{group}\Desinstalar {#MiAplicacion}"; Filename: "{uninstallexe}"
Name: "{userdesktop}\{#MiAplicacion}"; Filename: "{app}\{#MiEjecutable}"; Tasks: escritorio

[Run]
Filename: "{app}\{#MiEjecutable}"; Description: "Abrir {#MiAplicacion} ahora"; Flags: nowait postinstall skipifsilent

[Code]
var
  PaginaSql: TInputQueryWizardPage;

// .NET Framework 4.7.2 o superior (Release >= 461808)
function NetFramework472Instalado(): Boolean;
var
  Release: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release)
            and (Release >= 461808);
end;

function InitializeSetup(): Boolean;
begin
  Result := True;
  if not NetFramework472Instalado() then
  begin
    MsgBox('Este programa necesita .NET Framework 4.7.2 o superior.' + #13#10 +
           'Windows 10 (actualizado) y Windows 11 ya lo traen. Instálelo desde el sitio de Microsoft y vuelva a ejecutar el instalador.',
           mbError, MB_OK);
    Result := False;
  end;
end;

procedure InitializeWizard();
begin
  PaginaSql := CreateInputQueryPage(wpSelectDir,
    'Conexión a SQL Server',
    '¿En qué instancia de SQL Server se va a crear la base de datos?',
    'Se usa autenticación de Windows. La base "TrabajoDeDiploma-Vega" se crea sola la primera vez que se abre el programa.' + #13#10#13#10 +
    'Ejemplos:  .\SQLEXPRESS  (SQL Server Express, lo habitual en la sala)   ·   .  (instancia predeterminada)' + #13#10 +
    'Se puede cambiar después desde el menú Inicio > Olimpic > Configuración de conexión.');
  PaginaSql.Add('Instancia de SQL Server:', False);
  PaginaSql.Values[0] := '.\SQLEXPRESS';
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  if (CurPageID = PaginaSql.ID) and (Trim(PaginaSql.Values[0]) = '') then
  begin
    MsgBox('Ingrese el nombre de la instancia de SQL Server (por ejemplo .\SQLEXPRESS).', mbError, MB_OK);
    Result := False;
  end;
end;

// Reemplaza el valor de "Data Source=" en el .config instalado
procedure GuardarInstanciaEnConfig();
var
  Archivo, Instancia: String;
  Contenido: AnsiString;
  Texto: String;
  Inicio, Fin: Integer;
begin
  Archivo := ExpandConstant('{app}\{#MiEjecutable}.config');
  Instancia := Trim(PaginaSql.Values[0]);

  if not LoadStringFromFile(Archivo, Contenido) then
  begin
    MsgBox('No se encontró el archivo de configuración: ' + Archivo, mbError, MB_OK);
    Exit;
  end;

  Texto := String(Contenido);
  Inicio := Pos('Data Source=', Texto);
  if Inicio = 0 then
  begin
    MsgBox('No se encontró "Data Source=" en ' + Archivo + '. Edítelo a mano con el Bloc de notas.', mbError, MB_OK);
    Exit;
  end;

  Inicio := Inicio + Length('Data Source=');
  Fin := Inicio;
  while (Fin <= Length(Texto)) and (Texto[Fin] <> ';') and (Texto[Fin] <> '"') do
    Fin := Fin + 1;

  Delete(Texto, Inicio, Fin - Inicio);
  Insert(Instancia, Texto, Inicio);

  SaveStringToFile(Archivo, AnsiString(Texto), False);
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
    GuardarInstanciaEnConfig();
end;
