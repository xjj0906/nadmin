using Autofac;

namespace Nadmin.Common
{
    public sealed class Global
    {
        private static Global _instance;

        public static Global Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Global();
                }

                return _instance;
            }
        }

        public IContainer Container { get; private set; }

        private bool _isInited = false;

        private Global()
        {

        }

        public void Init(IContainer container)
        {
            if (_isInited) return;

            Container = container;
            _isInited = true;
        }
    }
}