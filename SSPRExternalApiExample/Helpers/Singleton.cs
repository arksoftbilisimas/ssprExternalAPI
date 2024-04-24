namespace SSPRExternalApiExample.Api.Helpers
{
    public class Singleton<T> where T : class, new()
    {
        #region Singleton members

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = Activator.CreateInstance<T>();

                return instance;
            }
        }

        #endregion
    }
}
