using System;
using ModestTree;

namespace Zenject
{
    public class FactoryProviderWrapper<TContract> : IFactory<TContract>
    {
        readonly IProvider _provider;
        readonly InjectContext _injectContext;

        public FactoryProviderWrapper(
            IProvider provider, InjectContext injectContext)
        {
            Assert.That(injectContext.MemberType.DerivesFromOrEqual<TContract>());

            _provider = provider;
            _injectContext = injectContext;
        }

        public TContract Create()
        {
            var instance = _provider.GetInstance(_injectContext);

            Assert.That(instance == null
                || instance.GetType().DerivesFromOrEqual(_injectContext.MemberType));

            return (TContract)instance;
        }
    }
}

