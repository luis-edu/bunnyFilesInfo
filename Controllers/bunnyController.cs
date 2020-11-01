using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BunnyAPI.Model;
using BunnyCDN.Net.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunnyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class bunnyController : ControllerBase
    {
        public async ValueTask<List<Series>> obterNomes()
        {
            var bunnyCDNStorage = new BunnyCDNStorage("api-animaflix", "754b2acf-9259-47e3-82c442a62716-ea28-4006", "de");
            var objects = await bunnyCDNStorage.GetStorageObjectsAsync("/api-animaflix/");
            List<Series> series = new List<Series>();

            foreach (var x in objects)
            {
                String cover = "";
                List<Episodio> episodes = new List<Episodio>();
                if (x.IsDirectory)
                {
                    var objs = await bunnyCDNStorage.GetStorageObjectsAsync("/api-animaflix/" + x.ObjectName);

                    foreach (var y in objs)
                    {
                        if (y.IsDirectory)
                        {
                            var cc = await bunnyCDNStorage.GetStorageObjectsAsync("/api-animaflix/" + x.ObjectName + "/" + y.ObjectName);
                            foreach (var b in cc)
                            {
                                cover = BuildURI(b.FullPath);
                            }

                        }
                        else
                        {
                            Episodio a = new Episodio();
                            a.Id = y.Guid;
                            a.Name = y.ObjectName;
                            a.Uri = BuildURI(y.FullPath);

                            episodes.Add(a);
                        }

                    }

                }
                Series aa = new Series();
                aa.Name = x.ObjectName;
                aa.eps = episodes;
                aa.cover = cover;
                series.Add(aa);
            }

            return series;
        }

        public string BuildURI(String s)
        {
            string molded = s.Remove(0, 15);
            string prefix = "https://animaflix.b-cdn.net/";
            return Uri.EscapeUriString(prefix + molded);
        }
        [HttpGet]
        public ValueTask<List<Series>> Get()
        {
            return obterNomes();
        }
    }
}
