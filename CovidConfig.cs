using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string satuan_suhu { get; set; } = "celcius";
    public int batas_hari_demam { get; set; } = 14;
    public string pesan_ditolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
    public string pesan_diterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

    private const string ConfigFileName = "covid_config.json";

    public CovidConfig()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        if (File.Exists(ConfigFileName))
        {
            string json = File.ReadAllText(ConfigFileName);
            try
            {
                var config = JsonSerializer.Deserialize<CovidConfig>(json);

                this.satuan_suhu = config?.satuan_suhu?.Trim().ToLower() ?? "celcius";
                this.batas_hari_demam = config?.batas_hari_demam != 0 ? config.batas_hari_demam : 14;
                this.pesan_ditolak = config?.pesan_ditolak ?? "Anda tidak diperbolehkan masuk ke dalam gedung ini";
                this.pesan_diterima = config?.pesan_diterima ?? "Anda dipersilahkan untuk masuk ke dalam gedung ini";
            }
            catch
            {
                Console.WriteLine("Gagal membaca konfigurasi. Menggunakan nilai default.");
            }
        }
    }

    public void UbahSatuan()
    {
        satuan_suhu = satuan_suhu == "celcius" ? "fahrenheit" : "celcius";
        Console.WriteLine($"Satuan suhu berhasil diubah ke: {satuan_suhu}");
    }
}
