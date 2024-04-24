using System.Diagnostics;

namespace SSPRExternalApiExample.Api.Helpers
{
    public class LogHelper : Singleton<LogHelper>
    {
        /// <summary>
        /// Write - Uygulamanın tüm exception'larını bu metod EventLog'a yazacaktır
        /// Fire and forget olduğu için dönüş tipi Task değildir. Uygulamanın bu davranışı beklemesi gereksizdir.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="isError"></param>
        public void Write(string log, int exceptionId, EventLogEntryType eventType = EventLogEntryType.Error, object data = null, string methodName = "")
        {
            Task.Run(() =>
            {
                try
                {
                    CheckEventSource();

                    log += data != null ? $"{Environment.NewLine}{JsonHelper.Instance.SerializeObject(data)}" : string.Empty;

                    log += $"{Environment.NewLine}MethodName: {methodName}";

                    EventLog.WriteEntry("SSPRExternalApiTestApi", log, eventType, exceptionId);
                }
                catch
                { }
            });
        }

        public void CheckEventSource()
        {
            try
            {
                if (!EventLog.SourceExists("SSPRExternalApiTestApi"))
                {
                    EventLog.CreateEventSource("SSPRExternalApiTestApi", "SSPRExternalApiTestApi");
                }
            }
            catch
            { }
        }
    }
}
