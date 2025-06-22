using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Domain.Base.Entity;
public class EntityBaseComparer : IEqualityComparer<EntityBase> {
    public bool Equals(EntityBase? x, EntityBase? y) {
        return x is not null && y is not null && x.Equals(y);
    }

    public int GetHashCode([DisallowNull] EntityBase obj) {
        return obj.GetHashCode();
    }
}
