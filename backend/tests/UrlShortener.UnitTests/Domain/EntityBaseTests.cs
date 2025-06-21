using Shouldly;
using UrlShortener.Domain.Base;

namespace UrlShortener.UnitTests.Domain;
public class EntityBaseTests {

    [Fact]
    public void Constructor_WithId_SetsId() {
        var entity = new TestEntity(5);
        entity.Id.ShouldBe(5);
    }

    [Fact]
    public void DefaultConstructor_SetsIdToDefault() {
        var entity = new TestEntity();
        entity.Id.ShouldBe(0);
    }

    [Fact]
    public void Equals_ReturnsTrue_ForSameIdAndType() {
        var a = new TestEntity(1);
        var b = new TestEntity(1);

        a.Equals(b).ShouldBeTrue();
        a.Equals((object)b).ShouldBeTrue();
    }

    [Fact]
    public void Equals_ReturnsFalse_WhenOtherIsNull() {
        var a = new TestEntity(1);

        a.Equals((EntityBase?)null).ShouldBeFalse();
        a.Equals((object?)null).ShouldBeFalse();
    }

    [Fact]
    public void Equals_ReturnsFalse_WhenOtherIsDifferentType() {
        var a = new TestEntity(1);
        var b = new DifferentEntity(1);

        a.Equals(b).ShouldBeFalse();
        a.Equals((object)b).ShouldBeFalse();
    }

    private class DifferentEntity : EntityBase {
        public DifferentEntity(int id) : base(id) { }
    }

    [Fact]
    public void Equals_ReturnsFalse_WhenDifferentId() {
        var a = new TestEntity(1);
        var b = new TestEntity(2);

        a.Equals(b).ShouldBeFalse();
        a.Equals((object)b).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldBeConsistentWithId() {
        var a = new TestEntity(2);
        var expected = 2.GetHashCode() * 41;

        a.GetHashCode().ShouldBe(expected);
    }

    [Fact]
    public void EqualityOperator_ReturnsTrue_ForSameId() {
        var a = new TestEntity(3);
        var b = new TestEntity(3);

        (a == b).ShouldBeTrue();
    }

    [Fact]
    public void EqualityOperator_ReturnsFalse_ForDifferentId() {
        var a = new TestEntity(3);
        var b = new TestEntity(4);

        (a != b).ShouldBeTrue();
    }

    [Fact]
    public void EqualityOperator_ReturnsTrue_WhenBothNull() {
        TestEntity? a = null;
        TestEntity? b = null;

        (a == b).ShouldBeTrue();
    }

    [Fact]
    public void EqualityOperator_ReturnsFalse_WhenOnlyOneNull() {
        TestEntity? a = new(1);
        TestEntity? b = null;

        (a == b).ShouldBeFalse();
        (b == a).ShouldBeFalse();
        (a != b).ShouldBeTrue();
        (b != a).ShouldBeTrue();
    }

    [Fact]
    public void IEqualityComparer_Equals_ReturnsTrue_ForEqualEntities() {
        var comparer = new TestEntity(1);
        var x = new TestEntity(1);
        var y = new TestEntity(1);

        comparer.Equals(x, y).ShouldBeTrue();
    }

    [Fact]
    public void IEqualityComparer_Equals_ReturnsFalse_IfEitherNull() {
        var comparer = new TestEntity(1);
        var x = new TestEntity(1);

        comparer.Equals(null, x).ShouldBeFalse();
        comparer.Equals(x, null).ShouldBeFalse();
    }

    [Fact]
    public void IEqualityComparer_GetHashCode_UsesEntityHashCode() {
        var entity = new TestEntity(5);
        IEqualityComparer<EntityBase> comparer = entity;

        comparer.GetHashCode(entity).ShouldBe(entity.GetHashCode());
    }

    public class TestEntity : EntityBase {
        public TestEntity(int id) : base(id) { }
        public TestEntity() { }
    }
}
