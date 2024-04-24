using SSPRExternalApiExample.Api.Constants;
using SSPRExternalApiExample.Api.Dtos;

namespace SSPRExternalApiExample.Api.Helpers
{
    /// <summary>
    /// External apinin cagirilmasi icin kullanilir.
    /// </summary>
    public class ApiHelper : Singleton<ApiHelper>
    {
        public async Task<List<DomainListItem>> GetAllDomains(CancellationTokenSource cToken)
        {
            try
            {
                var result = await RequestHelper<ResultItem, DomainListItem>.Instance.GetAsync($"{ExternalApiConstants.ExternalApiHost}{ExternalApiConstants.GetDomainPrefix}", cToken);

                if (result != null && result.IsOk && result.Data != null)
                {
                    var domainList = JsonHelper.Instance.DeserizalizeObject<List<DomainListItem>>(result.Data);

                    return domainList;
                }
                else if (result != null && !result.IsOk)
                {
                    LogHelper.Instance.Write(log: result.Message, methodName: System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, exceptionId: 9999);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Write(log: ex.ToString(), methodName: System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, exceptionId: 7023);
            }

            return null;
        }

        public async Task<ResultItem> ResetPasswordWithLink(ExternalResetPasswordDto externalResetPassword, CancellationTokenSource cToken)
        {
            try
            {
                var result = await RequestHelper<ResultItem, ExternalResetPasswordDto>.Instance.PostAsync(externalResetPassword, $"{ExternalApiConstants.ExternalApiHost}{ExternalApiConstants.ResetPasswordWithLinkPrefix}", cToken);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Write(log: ex.ToString(), methodName: System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, exceptionId: 7023);
            }

            return null;
        }

        public async Task<ResultItem> ResetPasswordWithRandomPassword(ExternalResetPasswordDto externalResetPassword, CancellationTokenSource cToken)
        {
            try
            {
                var result = await RequestHelper<ResultItem, ExternalResetPasswordDto>.Instance.PostAsync(externalResetPassword, $"{ExternalApiConstants.ExternalApiHost}{ExternalApiConstants.ResetPasswordWithRandomPasswordPrefix}", cToken);

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Write(log: ex.ToString(), methodName: System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, exceptionId: 7023);
            }

            return null;
        }
    }
}
