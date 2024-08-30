using l_winapi.InputOutput;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace WIndShellExperienceHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            string str = @"{
  ""Projects"": [
    {
      ""LastGithubVersion"": ""1.3.1"",
      ""JSON_OBJECT_GIT"": null,
      ""ButtonsTop"": [
        {
          ""Text"": ""Download"",
          ""Command"": ""window.open('https://github.com/Under4groos/SmdCompile.View/releases/download/1.3.1/SMDCompile.zip','_blank');""
        },
        {
          ""Text"": ""Вопросы? (TG)"",
          ""Command"": ""window.open('https://t.me/under4groos','_blank');""
        }
      ],
      ""Features"": [
        {
          ""Text"": ""Удобный UI"",
          ""Description"": ""Красивый и удобный дизайн. Все кнопки, фреймы, лейбы расположены такчтобы вам было удобно""
        },
        {
          ""Text"": "" Конвертация на лету "",
          ""Description"": ""Создание, удаление файлов, компиляция и конвертация материалов \\\""налету\\\"" и многое другое...""
        },
        {
          ""Text"": "" Простая настройка "",
          ""Description"": ""Простая настройка в пару кликов. Не нужно бегать по папкам и искать.exe шники, она сделает это за вас.""
        },
        {
          ""Text"": "" Редактор кода "",
          ""Description"": ""Полная настройка Текстового редактора. В папке \\\""Data\\\\SyntaxBox\\\""вы можете менять конфигурации форматов.""
        },
        {
          ""Text"": "" Консоль "",
          ""Description"": ""Удобный вывод логов в консоль. Возможно вынести в отдельное окно.""
        },
        {
          ""Text"": "" Множество настроек "",
          ""Description"": ""Доступная настройка каждого угла программы.""
        }
      ],
      ""Name"": ""SmdCompile"",
      ""Downloads"": 504,
      ""Description"": ""SmdCompile - автоматизация некоторых действий связанных с созданием QC,VTF, VMT файлов, с простой системой конвертации изображений, моделей ипрочего."",
      ""GitHubLinkReleases"": ""https://api.github.com/repos/Under4groos/SmdCompile.View/releases"",
      ""Images"": [
        ""smdcompile/smdcompile_1.png"",
        ""smdcompile/smdcompile_2.png"",
        ""smdcompile/smdcompile_3.png""
      ],
      ""YouTubeLinks"": [
        ""tnfaZXuzXls"",
        ""c0ZPQAf2Ir8"",
        ""ReLuqnsD7MU"",
        ""BM__TL2ru1Q""
      ]
    },
    {
      ""LastGithubVersion"": """",
      ""JSON_OBJECT_GIT"": null,
      ""ButtonsTop"": [
        {
          ""Text"": ""Cкачать"",
          ""Command"": ""window.open('https://underko.ru/files/file/WIndShellExperienceHost.Desktop.zip','_blank');""
        },
        {
          ""Text"": ""Вопросы? (TG)"",
          ""Command"": ""window.open('https://t.me/under4groos','_blank');""
        }
      ],
      ""Features"": [],
      ""Name"": ""WShellExp"",
      ""Downloads"": 0,
      ""Description"": ""Удобный инструмент для управления приложениями на вашем устройстве. Сего помощью вы сможете легко добавлять и удалять приложения. Благодаряпрограмме вы сможете легко и быстро открывать различные папки."",
      ""GitHubLinkReleases"": """",
      ""Images"": [
        ""WIndShellExperienceHost/1.png"",
        ""WIndShellExperienceHost/2.png""
      ],
      ""YouTubeLinks"": [
        ""RI-r6qs-rBs""
      ]
    },
    {
      ""LastGithubVersion"": """",
      ""JSON_OBJECT_GIT"": null,
      ""ButtonsTop"": [
        {
          ""Text"": ""Cкачать"",
          ""Command"": ""window.open('https://underko.ru/files/file/Hookbord.zip','_blank');""
        }
      ],
      ""Features"": [],
      ""Name"": ""Hookbord"",
      ""Downloads"": 0,
      ""Description"": "" Переопределение кнопок в Win 10 or 11 "",
      ""GitHubLinkReleases"": """",
      ""Images"": [
        ""Hookbord/1.png"",
        ""Hookbord/2.png""
      ],
      ""YouTubeLinks"": []
    }
  ]
}";

            File.WriteAllText("str.json", str);
            FIO.ReadFileToJsonObject("str.json", (o, e) =>
            {
                dynamic data = o as dynamic;

                if (data["Projects"] != null)
                {
                    foreach (var item in data["Projects"])
                    {
                        if (item["LastGithubVersion"] != null)
                        {
                            Debug.WriteLine(item["LastGithubVersion"] as object);
                        }

                    }
                }

            });

        }
    }
}