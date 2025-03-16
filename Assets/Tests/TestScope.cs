using TanitakaTech.NestedDIContainer;

namespace NestedDIContainer.Unity3d.Tests
{
    public class TestScope : IScope
    {
        public void Construct(DependencyBinder binder, object config)
        {
        }

        public ScopeId ScopeId { get; set; }
        public ScopeId? ParentScopeId { get; set; }

        public TestScope(ScopeId scopeId, ScopeId? parentScopeId)
        {
            ScopeId = scopeId;
            ParentScopeId = parentScopeId;
        }
    }
}