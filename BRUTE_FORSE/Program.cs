using System.Diagnostics;


string archivePath = "secret_file.7z";
string outputPath = "Секретная папка";
Directory.CreateDirectory(outputPath);
string sevenZipPath = @"C:\Program Files\7-Zip\7z.exe";
string letters = "abcdefghijklmnopqrstuvwxyz";
for (int i = 0; i<9999; i++) {
    foreach (char letter in letters) {
        string pass = i.ToString("D4")+letter;
        ProcessStartInfo psi = new ProcessStartInfo {
            FileName = sevenZipPath,
            Arguments = $"x \"{archivePath}\" -o\"{outputPath}\" -p{pass} -y",
            RedirectStandardError = true,
            RedirectStandardOutput = true,
        };
        using(Process process = Process.Start(psi)) {
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            if (output.Contains("Everything is Ok")) {
                Console.WriteLine($"Пароль найден: {pass}\nФайл успешно извлечён!");
                return;
            } else {
                Console.WriteLine($"Пароль {pass} не подходит");
            };
        };
    };
}
Console.WriteLine($"Не удалось подобрать пароль");
