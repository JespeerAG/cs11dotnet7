// See https://aka.ms/new-console-template for more information
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;

SectionTitle("* Handling cross-platform environments and filesystems");
string format = "{0, -33} {1}";
WriteLine(format, "Path.PathSeparator", PathSeparator);
WriteLine(format, "Path.DirectorySeparatorChar", DirectorySeparatorChar);
WriteLine(format, "Directory.GetCurrentDirectory()", GetCurrentDirectory());
WriteLine(format, "Environment.CurrentDirectory", CurrentDirectory);
WriteLine(format, "Environment.SystemDirectory", SystemDirectory);
WriteLine(format, "Path.GetTempPath()", GetTempPath());
WriteLine("GetFolderPath(SpecialFolder");
WriteLine(format, " .System)", GetFolderPath(SpecialFolder.System));
WriteLine(format, " .ApplicationData)", GetFolderPath(SpecialFolder.ApplicationData));
WriteLine(format, " .MyDocuments)", GetFolderPath(SpecialFolder.MyDocuments));
WriteLine(format, " .Personal)", GetFolderPath(SpecialFolder.Personal));
WriteLine(format, " .CommonDesktopDirectory)", GetFolderPath(SpecialFolder.CommonDesktopDirectory));

WriteLine();
SectionTitle("Managing drives");
string format2 = "{0, -30} | {1, -10} | {2, -7}, {3, 18:N0} | {4, 18:N0}";
WriteLine(format2, "NAME", "TYPE", "FORMAT", "SIZE (BYTES)", "FREE SPACE");

foreach (DriveInfo drive in DriveInfo.GetDrives())
{
    if (drive.IsReady)
    {
        WriteLine(format2, drive.Name, drive.DriveType, drive.DriveFormat, drive.TotalSize, drive.AvailableFreeSpace);
    }
    else
    {
        WriteLine("{0, -30} | {1, -10}", drive.Name, drive.DriveType);
    }
}

WriteLine();
SectionTitle("Managing directories");

string newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "NewFolder");

WriteLine($"Working with: {newFolder}");

WriteLine($"Does it exist? {Path.Exists(newFolder)}");

WriteLine("Creating it...");
CreateDirectory(newFolder);
WriteLine($"Does it exist? {Path.Exists(newFolder)}");
WriteLine("Confirm the directory exists, then press ENTER: ");
ReadLine();

WriteLine("Deleting it...");
Delete(newFolder, recursive: true);
WriteLine($"Does it exist? {Path.Exists(newFolder)}");

WriteLine();
SectionTitle("Managing files");

string dir = Combine(GetFolderPath(SpecialFolder.Personal), "OutputFiles");

CreateDirectory(dir);

string textFile = Combine(dir, "Dummy.txt");
string backupFile = Combine(dir, "Dummy.bak");
WriteLine($"Working with {textFile}");

WriteLine($"Does it exist? {File.Exists(textFile)}");

StreamWriter textWriter = File.CreateText(textFile);
textWriter.WriteLine("Hello, C#!");
textWriter.Close();
WriteLine($"Does it exist? {File.Exists(textFile)}");

File.Copy(sourceFileName: textFile, destFileName: backupFile, overwrite: true);

WriteLine($"Does {backupFile} exist? {File.Exists(backupFile)}");

Write("Confirm the files exist, then press ENTER: ");
ReadLine();

File.Delete(textFile);
WriteLine($"Does it exist? {File.Exists(textFile)}");

WriteLine($"Reading contents of {backupFile}:");
StreamReader textReader = File.OpenText(backupFile);
WriteLine(textReader.ReadToEnd());
textReader.Close();


WriteLine();
SectionTitle("Managing paths");

WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
WriteLine($"File Name: {GetFileName(textFile)}");
WriteLine($"File Name without Extension: {GetFileNameWithoutExtension(textFile)}");
WriteLine($"File Extension: {GetExtension(textFile)}");
WriteLine($"Random File Name: {GetRandomFileName()}");
WriteLine($"Temporary File Name: {GetTempFileName()}");

WriteLine();
SectionTitle("Getting file information");

FileInfo info = new(backupFile);
WriteLine($"{backupFile}:");
WriteLine($"Contains {info.Length} bytes");
WriteLine($"Last accessed {info.LastAccessTime}");
WriteLine($"Has readonly set to {info.IsReadOnly}");