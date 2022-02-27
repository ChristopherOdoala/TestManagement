using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TestManagement.Core.Context;
using TestManagement.Core.Dapper.Interfaces;
using TestManagement.Core.Dapper.Services;
using TestManagement.Core.Enums;
using TestManagement.Core.Models;
using TestManagement.Core.Services.Interfaces;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services
{
    public class TestReportingService : Service<PcrTestResults>, ITestReportingService
    {
        private readonly DataContext _dataContext;
        public TestReportingService(DataContext dataContext, IUnitOfWork unitOfWork) :base(unitOfWork)
        {
            _dataContext = dataContext;
        }

        public ResultModel<int> GetCases(int resultTypeId)
        {
            var resultModel = new ResultModel<int>();
            var parameters = new DynamicParameters();

            parameters.Add("@PcrTestResultTypedId", resultTypeId, DbType.Int32);

            try
            {
                var cases = ExecuteStoredProcedure<TestCaseViewModel>("[GetNoOfTestCases]", parameters).Result.ToList();
                resultModel.Data = cases.FirstOrDefault()?.TotalCount ?? 0;
                return resultModel;
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
                resultModel.Data = 0;
            }

            return resultModel;
        }

        public ResultModel<List<GetAllCaseTypeViewModel>> GetAllCaseType()
        {
            var resultModel = new ResultModel<List<GetAllCaseTypeViewModel>>();

            try
            {
                var testResults = _dataContext.PcrTestResults
                                    .GroupBy(x => x.PcrTestResultTypedId)
                                    .Select(t => new GetAllCaseTypeViewModel(t.Key)
                                    {
                                        TypeId = t.Key,
                                        Count = t.Count(),
                                    });

                resultModel.Data = testResults.ToList();
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;

        }
    }
}
