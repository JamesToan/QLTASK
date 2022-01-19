using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class YeuCauController : Controller
    {
        private ApplicationDbContext _context;

        public YeuCauController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = new UserClaim(HttpContext);
            var result = _context.YeuCau
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                //.Include(e => e.Status)
                .Include(e => e.User).FirstOrDefault();
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
        public IActionResult Select(int? StateId, int? DichVuId)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                if (DichVuId ==7 && StateId != 5)
                {
                    var result = _context.YeuCau.Where(e => e.StateId == StateId )
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.User)
                .OrderByDescending(e => e.Id)
                .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (StateId ==5 && DichVuId !=7)
                {
                    var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId )
                                   .Include(e => e.DichVu)
                                   .Include(e => e.NhanSu)
                                   .Include(e => e.States)
                                   .Include(e => e.User)
                                   .OrderByDescending(e => e.Id)
                                   .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (StateId != 5 && DichVuId != 7)
                {
                    var result = _context.YeuCau.Where(e => e.DichVuId == DichVuId && e.StateId == StateId)
                                  .Include(e => e.DichVu)
                                  .Include(e => e.NhanSu)
                                  .Include(e => e.States)
                                  .Include(e => e.User)
                                  .OrderByDescending(e => e.Id)
                                  .ToList();
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
                    var result = _context.YeuCau.Where(e => StateId == null
                || e.DichVuId == null || e.DichVuId == DichVuId || e.StateId == StateId)
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.User)
                .OrderByDescending(e => e.Id)
                .ToList();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Selectall(int? StateId, int? DichVuId)
        {
            var user = new UserClaim(HttpContext);
            if (user.RoleId == 1 || user.RoleId == 2)
            {
                
                    var result = _context.YeuCau.Where(e => StateId == null
                || e.DichVuId == null || e.DichVuId == DichVuId || e.StateId == StateId)
                .Include(e => e.DichVu)
                .Include(e => e.NhanSu)
                .Include(e => e.States)
                .Include(e => e.User)
                .Include(e=> e.DonViYeuCau)
                .OrderByDescending(e => e.Id)
                .ToList();
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
        public IActionResult Add([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user != null)
                {
                    model.NguoiTaoId = user.UserId;
                    model.NgayTao = DateTime.Now;
                   
                    
                    _context.Add(model);
                    _context.SaveChanges();

                   var idyeucau = _context.YeuCau.Select(x => x.Id).LastOrDefault();

                    Jira jr = new Jira();
                    jr.LinkJira = model.JiraDaGui;
                    jr.YeuCauId = idyeucau;
                    _context.Jira.Add(jr);
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
        public IActionResult Update([FromBody] YeuCau model)
        {
            try
            {
                var user = new UserClaim(HttpContext);
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    var result = _context.YeuCau.SingleOrDefault(e => e.Id == model.Id);
                    if (result != null) //update
                    {
                        result.TenYeuCau = model.TenYeuCau;
                        result.NoiDung = model.NoiDung;
                        result.ThoiHan = model.ThoiHan;
                        result.JiraDaGui = model.JiraDaGui;
                        result.NguoiGiamSatId = model.NguoiGiamSatId;
                        result.ThoiHanMongMuon = model.ThoiHanMongMuon;
                        result.FileUpload = model.FileUpload;
                        result.NgayYeuCau = model.NgayYeuCau;
                        result.StateId = model.StateId;
                        result.DichVuId = model.DichVuId;
                        result.DonViId = model.DonViId;
                        result.NguoiTaoId = user.UserId;
                        result.NgayCapNhat = DateTime.Now;

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
                var result = _context.YeuCau.SingleOrDefault(e => e.Id == id);
                var jira = _context.Jira.Where(p => p.LinkJira == result.JiraDaGui).FirstOrDefault();
                if (result != null) //update
                {
                    _context.Remove(jira);
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

        [HttpGet]
        public async Task<string> GetTrangThai(string majira)
        {

            //var client = new RestClient("https://cntt.vnpt.vn/rest/api/2/issue/IT360-224546");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Basic dG9hbmxtLmxhbjpKYW1lczAxMjM0NUA=");
            //request.AddHeader("Cookie", "BIGipServerPool_ttcntt.vnpt.vn_443=!g4vwiZq0EwOTof/necoQOJnflXX/zfOLbohW5wA9AzCdBwH+UQGxahaf+3dEzm92u+6rauG9ygANZ1E=; JSESSIONID=3A028B54660430287B45BA7B61233E4A; atlassian.xsrf.token=BNR2-A9FG-4XCX-SDI4_c3398ff1e416998278db04f70f8845df05a5899a_lin");
            //IRestResponse response = client.Execute(request);
            
            //Console.WriteLine(split);
            object userInfos = new { Username = "toanlm.lan", Password = "James012345@" };
                var client = new System.Net.Http.HttpClient();
                var jsonObj = JsonConvert.SerializeObject(userInfos);
                var connectionUrl = "https://cntt.vnpt.vn/rest/api/2/issue/" + majira;
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(connectionUrl),
                    Method = HttpMethod.Get
                };
                request.Headers.Add("Authorization", "Basic dG9hbmxtLmxhbjpKYW1lczAxMjM0NUA=");
                request.Headers.Add("Cookie", "BIGipServerPool_ttcntt.vnpt.vn_443=!g4vwiZq0EwOTof/necoQOJnflXX/zfOLbohW5wA9AzCdBwH+UQGxahaf+3dEzm92u+6rauG9ygANZ1E=; JSESSIONID=3A028B54660430287B45BA7B61233E4A; atlassian.xsrf.token=BNR2-A9FG-4XCX-SDI4_c3398ff1e416998278db04f70f8845df05a5899a_lin");
                var response = await client.SendAsync(request);
                
                var dataResult = response.Content.ReadAsStringAsync().Result;
                JObject joResponse = JObject.Parse(dataResult);
                
                Console.WriteLine(dataResult);
                return dataResult;
          
        }
    }
}
