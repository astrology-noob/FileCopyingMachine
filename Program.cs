using FileCopyingMachine;
using System.Text.Json;

AppConfiguration? configuration;
Logger logger = new();
string now = DateTime.Now.ToString().Replace(':', '-');

logger.CreateLogFile(now);

logger.WriteLog("App started", Logger.LogType.Info);

using (StreamReader r = new StreamReader("C:\\Users\\Мелания\\Documents\\FileCopyingMachine\\settings.json"))
{
    string json = r.ReadToEnd();
    configuration = JsonSerializer.Deserialize<AppConfiguration>(json);
}

logger.WriteLog("Configuration read", Logger.LogType.Info);

if (configuration == null)
{
    logger.WriteLog("Configuration not specified. Fatal Error", Logger.LogType.Error);
    throw new Exception("PathConfiguration not specified");
}

logger.appLogLevel = configuration.LogLevel;

string? target_dir_path = configuration.TargetDirectory;
List<string>? init_dir_paths = configuration.InitialDirectories;

if (!Directory.Exists(target_dir_path))
{
    logger.WriteLog("Target directory does not exist. Fatal Error", Logger.LogType.Error);
    throw new Exception("Target directory does not exist");
}

if (!Directory.Exists(target_dir_path))
{
    logger.WriteLog("Initial directories list was not specified. Fatal Error", Logger.LogType.Error);
    throw new Exception("Target directory does not exist");
}

foreach (string init_dir_path in init_dir_paths)
{
    if (!Directory.Exists(init_dir_path))
    {
        logger.WriteLog("Initial directory does not exist. Fatal Error", Logger.LogType.Error);
        throw new Exception("Initial directory does not exist");
    }

    string backup_directory;

    if (init_dir_paths.Count == 1)
    {
        backup_directory = target_dir_path + "/backup " + now;
    }

    else {
        string[] init_dir_path_arr = init_dir_path.Split('\\');
        backup_directory = target_dir_path + "/backup " + now + "/" + init_dir_path_arr[init_dir_path_arr.Length - 1];
    }

    Directory.CreateDirectory(backup_directory);
    logger.WriteLog("Backup directory created", Logger.LogType.Info);

    string[] files = Directory.GetFiles(init_dir_path);
    logger.WriteLog("Got list of files in directory", Logger.LogType.Debug);

    foreach (string file in files)
    {
        string[] fileNameArr = file.Split('\\');
        string newFile = backup_directory + "/" + fileNameArr[fileNameArr.Length - 1];
        File.Copy(file, newFile);
        logger.WriteLog("File " + fileNameArr[fileNameArr.Length - 1] + " copied", Logger.LogType.Debug);
    }
}

logger.WriteLog("App finished", Logger.LogType.Info);