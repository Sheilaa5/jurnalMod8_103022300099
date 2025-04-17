using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


    class BankTransferConfig
    {
        public string Lang { get; set; }
        public Transfer transfer { get; set; }
        public string[] method { get; set; }
        public Confirmation confirmation { get; set; }

        public class Transfer
        {
            public int threshold { get; set; }
            public int low_fee { get; set; }
            public int high_fee { get; set; }
        }
         public class Confirmation
         {
             public string en { get; set; }
            public string id { get; set; }
         }

    public BankTransferConfig() 
    { 
        Lang = "en";
        transfer = new Transfer();
        transfer.threshold = 25000000;
        transfer.low_fee = 6500;
        transfer.high_fee = 15000;
        confirmation = new Confirmation();
        method = new string[] { "RTO", "(real-time)", "SKN", "RTGS", "BI", "FAST" };
    }
    public static BankTransferConfig LoadOrDefault()  
    {
        CovidConfig config;

        if (File.Exists(ConfigFilePath))
        {
            try
            {
                string json = File.ReadAllText(ConfigFilePath);
                config = JsonSerializer.Deserialize<BankTransferConfig>(json);
                if (config == null)
                {
                    Console.WriteLine($"Peringatan: File '{ConfigFilePath}' tidak valid atau kosong. Menggunakan konfigurasi default.");
                    config = new CovidConfig();
                    config.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat membaca atau deserialisasi '{ConfigFilePath}': {ex.Message}. Menggunakan konfigurasi default.");
                config = new BankTransferConfig();
                config.Save();
            }
        }
        else
        {
            Console.WriteLine($"File konfigurasi '{ConfigFilePath}' tidak ditemukan. Membuat file dengan nilai default.");
            config = new BankTransferConfig();
            config.Save();
        }

        return config;
    }
    public void Save()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(ConfigFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saat menyimpan konfigurasi ke '{ConfigFilePath}': {ex.Message}");
        }
    }
}
    

    

    


