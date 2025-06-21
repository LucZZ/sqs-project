using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Domain.Base;
public abstract class EntityBase : IEqualityComparer<EntityBase> {

    public int Id { get; set; }

    protected EntityBase(int id) => Id = id;

    protected EntityBase() { }

    public static bool operator ==(EntityBase? first, EntityBase? second) =>
        (first is null && second is null) || (first is not null && second is not null && first.Equals(second));

    public static bool operator !=(EntityBase? first, EntityBase? second) =>
        !(first == second);

    public bool Equals(EntityBase? other) {
        if (other is null) {
            return false;
        }
        if (other.GetType() != GetType()) {
            return false;
        }
        return other.Id == Id;
    }

    public override bool Equals(object? obj) {
        if (obj is null) {
            return false;
        }
        if (obj.GetType() != GetType()) {
            return false;
        }
        if (obj is not EntityBase entity) {
            return false;
        }
        return entity.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;

    public bool Equals(EntityBase? x, EntityBase? y) => x is not null && y is not null && x.Equals(y);

    public int GetHashCode([DisallowNull] EntityBase obj) => obj.GetHashCode();
}