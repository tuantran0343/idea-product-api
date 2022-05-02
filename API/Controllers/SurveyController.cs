using API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    //[AllowCrossSiteAttribute]
    [AllowAnonymous]
    [RoutePrefix("api/survey")]
    public class SurveyController : ApiController
    {
        string _ConnectionString = string.Empty;
        public SurveyController()
        {
            _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            int pp = 0;
        }

        [AllowAnonymous]
        [Route("test-cnn")]
        public async Task<object> TestCnn()
        {
            UserDA userDA = new UserDA(_ConnectionString);
            return await userDA.CountData();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create-data-survey")]
        public async Task<object> CreateDataSurvey()
        {
            var fileData = HttpContext.Current.Request.Files.Count > 0 ?HttpContext.Current.Request.Files[0] : null;
            string Fullname =HttpContext.Current.Request.Form["Fullname"];
            string EmployeeNumber =HttpContext.Current.Request.Form["EmployeeNumber"];

            string Department =HttpContext.Current.Request.Form["Department"];
            string Phonenumber =HttpContext.Current.Request.Form["Phonenumber"];

            string Question_1 =HttpContext.Current.Request.Form["Question_1"];
            string Question_2 =HttpContext.Current.Request.Form["Question_2"];
            string Question_3 =HttpContext.Current.Request.Form["Question_3"];
            var Question_4 =HttpContext.Current.Request.Form["Question_4"];
            //var fileData = Question_4.Count > 0 ?HttpContext.Current.Request.Files[0] : null;

            if (fileData != null && fileData.ContentLength > 0)
            {
                var fileName = EmployeeNumber + Path.GetFileName(fileData.FileName);
                var path = Path.Combine(
                   HttpContext.Current.Server.MapPath("~/DataUpload/Files"),
                                      fileName
               );
                string path2 = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath($"~/DataUpload/Files"), fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/DataUpload/Files"));
                }
                if (System.IO.File.Exists(path))
                { System.IO.File.Delete(path); }
                fileData.SaveAs(path);
                UploadFileQuestion4Request requs = new UploadFileQuestion4Request();
                requs.FileAddress = "/DataUpload/Files/" + fileName;
                requs.EmployeeNumber = EmployeeNumber;
                requs.FileName = fileData.FileName;
                requs.FileType = fileData.ContentType;
                UserDA userDA = new UserDA(_ConnectionString);
                var result = await userDA.AddFileQuestion4(requs);
                if (result.ErrorCode == 0)
                {
                    AddDataSurveyRequest requsAdd = new AddDataSurveyRequest();
                    requsAdd.Fullname = Fullname;
                    requsAdd.EmployeeNumber = EmployeeNumber;
                    requsAdd.Department = Department;
                    requsAdd.Phonenumber = Phonenumber;

                    requsAdd.Question_1 = Question_1;
                    requsAdd.Question_2 = Question_2;
                    requsAdd.Question_3 = Question_3;
                    requsAdd.Question_4 = result.Data.RowId;
                    var resultAdd = await userDA.AddDataSurvey(requsAdd);
                    if (resultAdd.ErrorCode == 0)
                    {
                        return resultAdd;
                    }
                    else
                    {
                        result.ErrorMsg = "Đã có lỗi xảy ra, vui lòng thử lại";
                    }
                }
                else if (result.ErrorCode == 1)
                    result.ErrorMsg = "Đã có lỗi xảy ra, vui lòng thử lại";
                return result;
            }
            else
            {
                AddDataSurveyRequest requsAdd = new AddDataSurveyRequest();
                requsAdd.Fullname = Fullname;
                requsAdd.EmployeeNumber = EmployeeNumber;
                requsAdd.Department = Department;
                requsAdd.Phonenumber = Phonenumber;

                requsAdd.Question_1 = Question_1;
                requsAdd.Question_2 = Question_2;
                requsAdd.Question_3 = Question_3;
                UserDA userDA = new UserDA(_ConnectionString);
                var resultAdd = await userDA.AddDataSurvey(requsAdd);
                if (resultAdd.ErrorCode == 0)
                {
                    return resultAdd;
                }
                else
                {
                    resultAdd.ErrorMsg = "Đã có lỗi xảy ra, vui lòng thử lại";
                }
                return resultAdd;
            }

        }

    }



}