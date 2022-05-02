using API.ConnectioHelper;
using API.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models
{
    public class UserDA
    {
        public string _ConnectionString { get; set; }
        public UserDA(string _connect)
        {
            _ConnectionString = _connect;
        }

        public async Task<ClientResponse<int>> CountData()
        {
            string sql = string.Empty;
            DataTable dsSrc;
            try
            {
                sql = @"SELECT COUNT(*) FROM InfoSurvey";

                Dictionary<string, dynamic> Cond = new Dictionary<string, dynamic>();

                dsSrc = ConnectionHelper.SelectQueryToDataTable(sql, Cond, _ConnectionString);

                if (dsSrc == null)
                {
                    return new ClientResponse<int>
                    {
                        ErrorCode = 100,
                        ErrorMsg = "Hệ thống đang bận. Vui lòng liên hệ CSKH để được hỗ trợ."
                    };
                }

                if (dsSrc.Rows.Count == 0)
                {
                    return new ClientResponse<int>
                    {
                        ErrorCode = 100,
                        ErrorMsg = "Hệ thống đang bận. Vui lòng liên hệ CSKH để được hỗ trợ."
                    };
                }

                DataRow dr = dsSrc.Rows[0];

                return new ClientResponse<int>()
                {
                    ErrorCode = 0,
                    ErrorMsg = "Thành công.",
                    Data = int.Parse(dr[0].ToString())
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<int>()
                {
                    ErrorCode = 1,
                    ErrorMsg = "Thất bại: " + ex.Message,
                    Data = -1
                };
            }
        }


        /// <summary>
        /// Create user
        /// </summary>
        /// <returns></returns>
        public async Task<ClientResponse<UploadFileQuestion4Response>> AddFileQuestion4(UploadFileQuestion4Request data)
        {
            string sql = string.Empty;
            DataTable dsSrc;
            try
            {
                sql = @"EXEC [dbo].[Add_Data_File_Question4]@EmployeeNumber,@FileAddress,@FileType,@FileName";

                Dictionary<string, dynamic> Cond = new Dictionary<string, dynamic>();
                Cond.Add("EmployeeNumber", data.EmployeeNumber);
                Cond.Add("FileAddress", data.FileAddress);
                Cond.Add("FileType", data.FileType);
                Cond.Add("FileName", data.FileName);

                dsSrc = ConnectionHelper.SelectQueryToDataTable(sql, Cond, _ConnectionString);

                if (dsSrc == null)
                {
                    return new ClientResponse<UploadFileQuestion4Response>
                    {
                        ErrorCode = 100,
                        ErrorMsg = "Hệ thống đang bận. Vui lòng liên hệ CSKH để được hỗ trợ."
                    };
                }

                if(dsSrc.Rows.Count == 0)
                {
                    return new ClientResponse<UploadFileQuestion4Response>
                    {
                        ErrorCode = 100,
                        ErrorMsg = "Hệ thống đang bận. Vui lòng liên hệ CSKH để được hỗ trợ."
                    };
                }

                DataRow dr = dsSrc.Rows[0];

                UploadFileQuestion4Response result = new UploadFileQuestion4Response
                {
                    RowId = int.Parse(dr["RowId"].ToString()),
                    FileAddress = dr["FileAddress"].ToString(),
                    FileName = dr["FileName"].ToString(),
                    FileType = dr["FileType"].ToString(),
                    EmployeeNumber = dr["EmployeeNumber"].ToString(),
                    DateCreate = DateTime.Parse(dr["DateCreate"].ToString())
                };

                return new ClientResponse<UploadFileQuestion4Response>()
                {
                    ErrorCode = 0,
                    ErrorMsg = "Thành công.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<UploadFileQuestion4Response>()
                {
                    ErrorCode = 1,
                    ErrorMsg = "Thất bại: " + ex.Message,
                    Data = null
                };
            }
        }


        public async Task<ClientResponse<AddDataSurveyResponse>> AddDataSurvey(AddDataSurveyRequest data)
        {
            string sql = string.Empty;
            DataTable dsSrc;
            try
            {
                sql = @"EXEC [dbo].[Add_Info_Survey] @Fullname,@EmployeeNumber,@Department,@Phonenumber,@Question_1,@Question_2,@Question_3,@Question_4";

                Dictionary<string, dynamic> Cond = new Dictionary<string, dynamic>();
                Cond.Add("Fullname", data.Fullname);
                Cond.Add("EmployeeNumber", data.EmployeeNumber);
                Cond.Add("Department", data.Department);
                Cond.Add("Phonenumber", data.Phonenumber);
                Cond.Add("Question_1", data.Question_1);
                Cond.Add("Question_2", data.Question_2);
                Cond.Add("Question_3", data.Question_3);
                Cond.Add("Question_4", data.Question_4);

                dsSrc = ConnectionHelper.SelectQueryToDataTable(sql, Cond, _ConnectionString);

                if (dsSrc == null)
                {
                    return new ClientResponse<AddDataSurveyResponse>
                    {
                        ErrorCode = 100,
                        ErrorMsg = "Hệ thống đang bận. Vui lòng liên hệ CSKH để được hỗ trợ."
                    };
                }

                if (dsSrc.Rows.Count == 0)
                {
                    return new ClientResponse<AddDataSurveyResponse>
                    {
                        ErrorCode = 100,
                        ErrorMsg = "Hệ thống đang bận. Vui lòng liên hệ CSKH để được hỗ trợ."
                    };
                }

                DataRow dr = dsSrc.Rows[0];

                AddDataSurveyResponse result = new AddDataSurveyResponse
                {
                    RowId = int.Parse(dr["RowId"].ToString()),
                    Fullname = dr["Fullname"].ToString(),
                    EmployeeNumber = dr["EmployeeNumber"].ToString(),
                    Department = dr["Department"].ToString(),
                    Phonenumber = dr["Phonenumber"].ToString(),
                    Question_1 = dr["Question_1"].ToString(),
                    Question_2 = dr["Question_2"].ToString(),
                    Question_3 = dr["Question_3"].ToString(),
                    Question_4 = dr["Question_4"].ToString(),
                    DateCreate = DateTime.Parse(dr["DateCreate"].ToString())
                };

                return new ClientResponse<AddDataSurveyResponse>()
                {
                    ErrorCode = 0,
                    ErrorMsg = "Thành công.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ClientResponse<AddDataSurveyResponse>()
                {
                    ErrorCode = 1,
                    ErrorMsg = "Thất bại: " + ex.Message,
                    Data = null
                };
            }
        }




        

    }

    public class UploadFileQuestion4Request
    {
        public string EmployeeNumber { get; set; }
        public string FileAddress { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
    }

    public class UploadFileQuestion4Response
    {
        public int RowId { get; set; }
        public string EmployeeNumber { get; set; }
        public string FileAddress { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreate { get; set; }
    }

    public class AddDataSurveyRequest
    {
        public string Fullname { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public string Phonenumber { get; set; }
        public string Question_1 { get; set; }
        public string Question_2 { get; set; }
        public string Question_3 { get; set; }
        public int Question_4 { get; set; }
    }


    public class AddDataSurveyResponse
    {
        public int RowId { get; set; }
        public string Fullname { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public string Phonenumber { get; set; }
        public string Question_1 { get; set; }
        public string Question_2 { get; set; }
        public string Question_3 { get; set; }
        public string Question_4 { get; set; }
        public DateTime DateCreate { get; set; }
    }



}