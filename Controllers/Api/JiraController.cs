using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers.Api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JiraController : ControllerBase
    {
        private ApplicationDbContext _context;

        public JiraController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.Jira.SingleOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        public IActionResult Select(int ycid)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
            {
                var result = _context.Jira.Where(j => j.YeuCauId == ycid).Include(e => e.YeuCau).ToList();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Add([FromBody] Jira model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user != null)
                {
                    _context.Add(model);
                    _context.SaveChanges();
                    return Ok(model.Id);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] Jira model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2 || user.RoleId == 3)
                {
                    var result = _context.Jira.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.TenJira = model.TenJira;
                        result.LinkJira = model.LinkJira;
                        result.YeuCauId = model.YeuCauId;
                        result.IsActive = model.IsActive;
                        _context.Update(result);
                        _context.SaveChanges();
                        return Ok(result.Id);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _context.Jira.SingleOrDefault(e => e.Id == id);
                if (result != null) //update
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                    return Ok(1);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public async Task<ActionResult> SaveCommentAsync()
        {
            var getlist = _context.Jira.ToList();
           
            var userinfo = _context.User.Where(p => p.isActive == true ).Select(p=> new {p.Id, p.JiraAcount, p.JiraPass }).ToList();

            var yeucau = _context.YeuCau.Select(p => new { p.NhanSuId, p.Id }).ToList();

            var nhansu = _context.NhanSu.Select(p => new {p.Id,p.UserId }).ToList();

            DateTime date1 = new DateTime(2022, 8, 17,15,0,0);
            //DateTime date2 = new DateTime(2022, 7, 2);
            var jiraacount = _context.User.Where(p => p.Id == 1080).Select(p => new { p.Id, p.JiraAcount, p.JiraPass }).FirstOrDefault();
            var listyeucau = _context.YeuCau.Where(p => p.StateId != 9 && p.JiraDaGui != null  && p.NgayTao > date1).ToList();
            var comment = "";
            //foreach (var uname in userinfo)
            //{
            //    if (uname.JiraAcount != null && uname.JiraPass != null)
            //    {
            //        foreach (var item in getlist)
            //        {
            //            foreach (var item1 in listyeucau)
            //            {
            //                if (item.YeuCauId == item1.Id)
            //                {
            //                    var jiralink = item.LinkJira;
            //                    var split = jiralink.Split('/');
            //                    var majira = "";
            //                    for (var i = 0; i < split.Length; i++)
            //                    {
            //                        majira = split[split.Length - 1];
            //                    }

            //                    object userInfos = new { Username = uname.JiraAcount, Password = uname.JiraPass };
            //                    var byteArray = Encoding.ASCII.GetBytes($"{uname.JiraAcount}:{uname.JiraPass}");
            //                    string encodeString = "Basic " + Convert.ToBase64String(byteArray);
            //                    var client = new System.Net.Http.HttpClient();
            //                    var jsonObj = JsonConvert.SerializeObject(userInfos);
            //                    var connectionUrl = "https://cntt.vnpt.vn/rest/api/2/issue/" + majira;
            //                    StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
            //                    var request = new HttpRequestMessage()
            //                    {
            //                        RequestUri = new Uri(connectionUrl),
            //                        Method = HttpMethod.Get
            //                    };
            //                    request.Headers.Add("Authorization", encodeString);
            //                    var response = await client.SendAsync(request);

            //                    var dataResult = response.Content.ReadAsStringAsync().Result;
            //                    JObject joResponse = JObject.Parse(dataResult);
            //                    if (joResponse["fields"] != null)
            //                    {
            //                        JObject jObjectnew = (JObject)joResponse["fields"]["comment"];
            //                        JArray userResponseArray = (JArray)jObjectnew["comments"];

            //                        foreach (JObject ja in userResponseArray.Children<JObject>())
            //                        {
            //                            foreach (JProperty val in ja.Properties())
            //                            {
            //                                if (val.Name == "body")
            //                                {
            //                                    comment += (string)val.Value;
            //                                }
            //                                if (val.Name == "updateAuthor")
            //                                {
            //                                    var userName = val.Value["displayName"];
            //                                    comment += " - " + userName.ToString();
            //                                }
            //                                if (val.Name == "updated")
            //                                {
            //                                    var time = val.Value;
            //                                    var date = time.ToString();
            //                                    DateTime enteredDate = DateTime.Parse(date);
            //                                    var format = enteredDate.ToString("dd/MM/yyyy");
            //                                    comment += " - " + (string)time.ToString() + "; ";
            //                                }

            //                            }

            //                        }

            //                        item.Comment = comment;
            //                        _context.Update(item);
            //                        _context.SaveChanges();
            //                        comment = "";
            //                    }

            //                }
            //            }

            //        }
            //    }
            //}
            foreach (var item in getlist)
            {
                foreach (var item1 in listyeucau)
                {
                    if (item.YeuCauId == item1.Id)
                    {
                        var jiralink = item.LinkJira;
                        var split = jiralink.Split('/');
                        var majira = "";
                        
                        for (var i = 0; i < split.Length; i++)
                        {
                            majira = split[split.Length - 1];
                        }

                        object userInfos = new { Username = jiraacount.JiraAcount, Password = jiraacount.JiraPass };
                        var byteArray = Encoding.ASCII.GetBytes($"{jiraacount.JiraAcount}:{jiraacount.JiraPass}");
                        string encodeString = "Basic " + Convert.ToBase64String(byteArray);
                        var client = new System.Net.Http.HttpClient();
                        var jsonObj = JsonConvert.SerializeObject(userInfos);
                        var connectionUrl = "https://cntt.vnpt.vn/rest/api/2/issue/" + majira;
                        StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                        var request = new HttpRequestMessage()
                        {
                            RequestUri = new Uri(connectionUrl),
                            Method = HttpMethod.Get
                        };
                        request.Headers.Add("Authorization", encodeString);
                        var response = await client.SendAsync(request);

                        var dataResult = response.Content.ReadAsStringAsync().Result;
                        JObject joResponse = JObject.Parse(dataResult);
                        if (joResponse["fields"] != null)
                        {
                            JObject jObjectnew = (JObject)joResponse["fields"]["comment"];
                            JArray userResponseArray = (JArray)jObjectnew["comments"];
                            JObject jObjectnew1 = (JObject)joResponse["fields"]["status"];
                            string status = jObjectnew1["name"].Value<string>();

                            if (status != "Closed" && status != "Cancelled" && status != "Resolved")
                            {
                                foreach (JObject ja in userResponseArray.Children<JObject>())
                                {
                                    foreach (JProperty val in ja.Properties())
                                    {
                                        if (val.Name == "body")
                                        {
                                            comment += (string)val.Value;
                                        }
                                        if (val.Name == "updateAuthor")
                                        {
                                            var userName = val.Value["displayName"];
                                            comment += " - " + userName.ToString();
                                        }
                                        if (val.Name == "updated")
                                        {
                                            var time = val.Value;
                                            var date = time.ToString();
                                            DateTime enteredDate = DateTime.Parse(date);
                                            var format = enteredDate.ToString("dd/MM/yyyy");
                                            comment += " - " + (string)time.ToString() + "; ";
                                        }

                                    }

                                }

                                item.Comment = comment;
                                _context.Update(item);
                                _context.SaveChanges();
                                comment = "";
                            }
                            
                        }

                    }
                }

            }

            return Ok();
        }

        public ActionResult callSaveComment()
        {
            var JobName = "SaveComment";
            // first job
            RecurringJob.AddOrUpdate(
                JobName,
                () => SaveCommentAsync(),
                Cron.Hourly);
            return Ok();
        }
    }
}
