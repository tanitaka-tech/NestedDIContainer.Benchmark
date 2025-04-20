using Doinject;
using ManualDi.Main;
using NestedDIContainer.Unity3d.Tests.PinInject;
using NUnit.Framework;
using TanitakaTech.NestedDIContainer;
using Unity.PerformanceTesting;
using VContainer;
using ContainerBuilder = Reflex.Core.ContainerBuilder;

namespace NestedDIContainer.Unity3d.Tests
{
    public class ContainerPerformanceTest
    {
        const int MeasurementCount = 100;
        const int WarmupCount = 100;

        [Test]
        [Performance]
        [Timeout(int.MaxValue)]
        public void Benchmark()
        {
            Measure
                .Method(() =>
                {
                    var manualDiBuilder = new ManualDi.Main.DiContainerBindings();
                    manualDiBuilder.Bind<IFirstService, FirstService>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISecondService, SecondService>().Default().FromConstructor();
                    manualDiBuilder.Bind<IThirdService, ThirdService>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISubObjectA, SubObjectA>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISubObjectB, SubObjectB>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISubObjectC, SubObjectC>().Default().FromConstructor();
                    manualDiBuilder.Bind<IComplex1, Complex1>().Default().FromConstructor();
                    manualDiBuilder.Bind<IComplex2, Complex2>().Default().FromConstructor();
                    manualDiBuilder.Bind<IComplex3, Complex3>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISubObjectOne, SubObjectOne>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISubObjectTwo, SubObjectTwo>().Default().FromConstructor();
                    manualDiBuilder.Bind<ISubObjectThree, SubObjectThree>().Default().FromConstructor();
                    var manualDiContainer = manualDiBuilder.Build();

                    manualDiContainer.Resolve<IComplex1>();
                    manualDiContainer.Resolve<IComplex2>();
                    manualDiContainer.Resolve<IComplex3>();
                })
                .SampleGroup(new SampleGroup("ManualDi", SampleUnit.Nanosecond))
                .GC()
                .MeasurementCount(MeasurementCount)
                .WarmupCount(WarmupCount)
                .Run();

            Measure
                .Method(() =>
                {
                    var reflexContainer = new ContainerBuilder();
                    reflexContainer.AddSingleton(typeof(FirstService), typeof(IFirstService));
                    reflexContainer.AddSingleton(typeof(SecondService), typeof(ISecondService));
                    reflexContainer.AddSingleton(typeof(ThirdService), typeof(IThirdService));
                    reflexContainer.AddSingleton(typeof(SubObjectA), typeof(ISubObjectA));
                    reflexContainer.AddSingleton(typeof(SubObjectB), typeof(ISubObjectB));
                    reflexContainer.AddSingleton(typeof(SubObjectC), typeof(ISubObjectC));
                    reflexContainer.AddSingleton(typeof(Complex1), typeof(IComplex1));
                    reflexContainer.AddSingleton(typeof(Complex2), typeof(IComplex2));
                    reflexContainer.AddSingleton(typeof(Complex3), typeof(IComplex3));
                    reflexContainer.AddSingleton(typeof(SubObjectOne), typeof(ISubObjectOne));
                    reflexContainer.AddSingleton(typeof(SubObjectTwo), typeof(ISubObjectTwo));
                    reflexContainer.AddSingleton(typeof(SubObjectThree), typeof(ISubObjectThree));

                    var a = reflexContainer.Build();
                    a.Resolve<IComplex1>();
                    a.Resolve<IComplex2>();
                    a.Resolve<IComplex3>();
                })
                .SampleGroup(new SampleGroup("Reflex", SampleUnit.Nanosecond))
                .GC()
                .MeasurementCount(MeasurementCount)
                .WarmupCount(WarmupCount)
                .Run();

            MeasureNestedDIContainer();

            MeasurePinInject();

            Measure
                .Method(() =>
                {
                    var vContainerBuilder = new VContainer.ContainerBuilder();
                    
                    var firstService = new FirstService();
                    var secondService = new SecondService();
                    var thirdService = new ThirdService();
                    var subObjectA = new SubObjectA(new ServiceA());
                    var subObjectB = new SubObjectB(new ServiceB());
                    var subObjectC = new SubObjectC(new ServiceC());
                    var subObjectOne = new SubObjectOne(firstService);
                    var subObjectTwo = new SubObjectTwo(secondService);
                    var subObjectThree = new SubObjectThree(thirdService);
                    vContainerBuilder.RegisterInstance<IFirstService>(firstService);
                    vContainerBuilder.RegisterInstance<ISecondService>(secondService);
                    vContainerBuilder.RegisterInstance<IThirdService>(thirdService);
                    vContainerBuilder.RegisterInstance<ISubObjectA>(subObjectA);
                    vContainerBuilder.RegisterInstance<ISubObjectB>(subObjectB);
                    vContainerBuilder.RegisterInstance<ISubObjectC>(subObjectC);
                    vContainerBuilder.RegisterInstance<ISubObjectOne>(subObjectOne);
                    vContainerBuilder.RegisterInstance<ISubObjectTwo>(subObjectTwo);
                    vContainerBuilder.RegisterInstance<ISubObjectThree>(subObjectThree);
                    vContainerBuilder.RegisterInstance<IComplex1>(new Complex1(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    vContainerBuilder.RegisterInstance<IComplex2>(new Complex2(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    vContainerBuilder.RegisterInstance<IComplex3>(new Complex3(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    var vContainer = vContainerBuilder.Build();

                    vContainer.Resolve<IComplex1>();
                    vContainer.Resolve<IComplex2>();
                    vContainer.Resolve<IComplex3>();
                })
                .SampleGroup(new SampleGroup("VContainer", SampleUnit.Nanosecond))
                .GC()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
            
            Measure
                .Method(async () =>
                {
                    var diContainer = new DIContainer();
                    
                    var firstService = new FirstService();
                    var secondService = new SecondService();
                    var thirdService = new ThirdService();
                    var subObjectA = new SubObjectA(new ServiceA());
                    var subObjectB = new SubObjectB(new ServiceB());
                    var subObjectC = new SubObjectC(new ServiceC());
                    var subObjectOne = new SubObjectOne(firstService);
                    var subObjectTwo = new SubObjectTwo(secondService);
                    var subObjectThree = new SubObjectThree(thirdService);
                    diContainer.BindFromInstance<IFirstService>(firstService);
                    diContainer.BindFromInstance<ISecondService>(secondService);
                    diContainer.BindFromInstance<IThirdService>(thirdService);
                    diContainer.BindFromInstance<ISubObjectA>(subObjectA);
                    diContainer.BindFromInstance<ISubObjectB>(subObjectB);
                    diContainer.BindFromInstance<ISubObjectC>(subObjectC);
                    diContainer.BindFromInstance<ISubObjectOne>(subObjectOne);
                    diContainer.BindFromInstance<ISubObjectTwo>(subObjectTwo);
                    diContainer.BindFromInstance<ISubObjectThree>(subObjectThree);
                    diContainer.BindFromInstance<IComplex1>(new Complex1(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    diContainer.BindFromInstance<IComplex2>(new Complex2(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    diContainer.BindFromInstance<IComplex3>(new Complex3(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));

                    await diContainer.ResolveAsync<IComplex1>();
                    await diContainer.ResolveAsync<IComplex2>();
                    await diContainer.ResolveAsync<IComplex3>();
                })
                .SampleGroup(new SampleGroup("DoInject", SampleUnit.Nanosecond))
                .GC()
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }

        [Test]
        [Performance]
        [Timeout(int.MaxValue)]
        public void BenchmarkOnlyNestedDIContainer()
        {
            MeasureNestedDIContainer();
        }
        
        [Test]
        [Performance]
        [Timeout(int.MaxValue)]
        public void BenchmarkAgainstPinInject()
        {
            MeasureNestedDIContainer();
            MeasurePinInject();
        }

        private void MeasureNestedDIContainer()
        {
            Measure
                .Method(() =>
                {
                    var scopeId = ScopeId.Create();
                    var scope = new TestScope(scopeId, null);
                    var scopeContainer = new ScopeContainer(scope, null);
                    var childBinder = new DependencyBinder(scopeContainer);

                    var firstService = new FirstService();
                    var secondService = new SecondService();
                    var thirdService = new ThirdService();
                    var subObjectA = new SubObjectA(new ServiceA());
                    var subObjectB = new SubObjectB(new ServiceB());
                    var subObjectC = new SubObjectC(new ServiceC());
                    var subObjectOne = new SubObjectOne(firstService);
                    var subObjectTwo = new SubObjectTwo(secondService);
                    var subObjectThree = new SubObjectThree(thirdService);
                    childBinder.Bind<IFirstService>(firstService);
                    childBinder.Bind<ISecondService>(secondService);
                    childBinder.Bind<IThirdService>(thirdService);
                    childBinder.Bind<ISubObjectA>(subObjectA);
                    childBinder.Bind<ISubObjectB>(subObjectB);
                    childBinder.Bind<ISubObjectC>(subObjectC);
                    childBinder.Bind<ISubObjectOne>(subObjectOne);
                    childBinder.Bind<ISubObjectTwo>(subObjectTwo);
                    childBinder.Bind<ISubObjectThree>(subObjectThree);
                    childBinder.Bind<IComplex1>(new Complex1(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    childBinder.Bind<IComplex2>(new Complex2(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    childBinder.Bind<IComplex3>(new Complex3(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));

                    scopeContainer.Resolve<IComplex1>();
                    scopeContainer.Resolve<IComplex2>();
                    scopeContainer.Resolve<IComplex3>();
                })
                .SampleGroup(new SampleGroup("NestedDIContainer", SampleUnit.Nanosecond))
                .GC()
                .MeasurementCount(MeasurementCount)
                .WarmupCount(WarmupCount)
                .Run();
        }
        
        private void MeasurePinInject()
        {
            Measure
                .Method(() =>
                {
                    var container = new DependencyContainerCopy();
                    var childBinder = new Cathei.PinInject.DependencyBinder(container);

                    var firstService = new FirstService();
                    var secondService = new SecondService();
                    var thirdService = new ThirdService();
                    var subObjectA = new SubObjectA(new ServiceA());
                    var subObjectB = new SubObjectB(new ServiceB());
                    var subObjectC = new SubObjectC(new ServiceC());
                    var subObjectOne = new SubObjectOne(firstService);
                    var subObjectTwo = new SubObjectTwo(secondService);
                    var subObjectThree = new SubObjectThree(thirdService);
                    childBinder.Bind<IFirstService>(firstService);
                    childBinder.Bind<ISecondService>(secondService);
                    childBinder.Bind<IThirdService>(thirdService);
                    childBinder.Bind<ISubObjectA>(subObjectA);
                    childBinder.Bind<ISubObjectB>(subObjectB);
                    childBinder.Bind<ISubObjectC>(subObjectC);
                    childBinder.Bind<ISubObjectOne>(subObjectOne);
                    childBinder.Bind<ISubObjectTwo>(subObjectTwo);
                    childBinder.Bind<ISubObjectThree>(subObjectThree);
                    childBinder.Bind<IComplex1>(new Complex1(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    childBinder.Bind<IComplex2>(new Complex2(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));
                    childBinder.Bind<IComplex3>(new Complex3(firstService, secondService, thirdService, subObjectOne, subObjectTwo, subObjectThree));

                    container.Resolve(typeof(IComplex1), null);
                    container.Resolve(typeof(IComplex2), null);
                    container.Resolve(typeof(IComplex3), null);
                })
                .SampleGroup(new SampleGroup("PinInject", SampleUnit.Nanosecond))
                .GC()
                .MeasurementCount(MeasurementCount)
                .WarmupCount(WarmupCount)
                .Run();
        }
    }
}