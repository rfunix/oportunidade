using System;
using System.Net;
using System.Text;
using MinutosSeguros.Service;

namespace MinutosSeguros
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                var pageContent = client.DownloadString("http://www.minutoseguros.com.br/blog/feed/");

                var bytes = Encoding.Default.GetBytes(pageContent);

                pageContent = Encoding.UTF8.GetString(bytes);

                var listaItens = Operations.ReturnDataAnalys(pageContent);

                foreach (var item in listaItens)
                {

                    var fullTextTopic = Operations.ReturnFullTextTopic(item);
                    var listTopTen = Operations.ReturnTopTenWords(fullTextTopic);
                    var qtdWordTopic = Operations.ReturnQtdWordTopic(fullTextTopic);

                    Console.WriteLine("Tópico: {0}\n", Operations.GetTitleTopic(item));
                    Console.WriteLine("Quantidade Total de palavras do Tópico: {0}\n", qtdWordTopic);
                    Console.WriteLine("Top 10 principais palavras: \n");
                    foreach (var wordTopic in listTopTen)
                    {
                        Console.WriteLine("+ Palavra: {0} , Ocorrências: {1}", wordTopic.word, wordTopic.ocurrenc);
                    }
                    Console.WriteLine("--------------------------------------------------------------------");

                   
                }
                Console.ReadKey();

            }

        }

    }




}
