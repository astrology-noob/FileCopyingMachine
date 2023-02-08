using FileCopyingMachine;
using System.Text.Json;

AppConfiguration configuration;
string now = DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss");

Logger.CreateLogFile(now);

using (StreamReader r = new StreamReader("C:\\Users\\Мелания\\Documents\\FileCopyingMachine\\settings.json"))
{
    string json = r.ReadToEnd();
    configuration = JsonSerializer.Deserialize<AppConfiguration>(json) ?? new();
}

if (configuration.InitialDirectories == null)
{
    ExceptionHandler.HandleException("Initial Directories were not specified");
}

if (configuration.TargetDirectory == null)
{
    ExceptionHandler.HandleException("Target Directory was not specified");
}

if (configuration.LogLevel == null)
{
    ExceptionHandler.HandleException("LogLevel was not specified");
}

List<string> initDirPathList = configuration.InitialDirectories!;
string targetDirPath = configuration.TargetDirectory!;

try
{
    Logger.AppLogLevel = Enum.Parse<Logger.LogType>(configuration.LogLevel!);
}
catch
{
    ExceptionHandler.HandleException("Specified LogLevel is not supported");
}

if (!Directory.Exists(targetDirPath))
{
    ExceptionHandler.HandleException("Target directory " + targetDirPath + " does not exist");
}

foreach (var initDirPath in initDirPathList)
{
    if (!Directory.Exists(initDirPath))
    {
        ExceptionHandler.HandleException("Initial directory " + initDirPath + " does not exist");
    }

    string backupDirectory = targetDirPath + "/backup " + now;

    string[] initDirPathArr = initDirPath.Split('\\');
    backupDirectory += "\\" + initDirPathArr[^1];

    Directory.CreateDirectory(backupDirectory);
    Logger.WriteLog("Backup directory created", Logger.LogType.Info);

    string[] files = Directory.GetFiles(initDirPath);
    Logger.WriteLog("Got list of files in directory", Logger.LogType.Debug);

    foreach (var file in files)
    {
        string[] fileNameArr = file.Split('\\');
        string newFile = backupDirectory + "\\" + fileNameArr[^1];
        File.Copy(file, newFile);
        Logger.WriteLog("File " + file + " copied", Logger.LogType.Debug);
    }
}

Logger.WriteLog("App finished", Logger.LogType.Info);