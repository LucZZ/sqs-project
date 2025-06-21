using NetArchTest.Rules;

namespace UrlShortener.ArchitectureTests;
public class DependenciesTests {

    [Fact]
    public void Domain_Should_Not_Have_Dependencies() {

        // Arrange
        var assembly = typeof(Domain.IAssemblyReference).Assembly;

        var otherProjects = new[] {
            Namespaces.ApplicationNamespace,
            Namespaces.InfrastructureNamespace,
            Namespaces.PersistenceNamespace,
            Namespaces.PresentationNamespace,
        };

        // Act
        var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Application_Should_Not_Have_Dependencies() {

        // Arrange
        var assembly = typeof(Application.IAssemblyReference).Assembly;

        var otherProjects = new[] {
            Namespaces.InfrastructureNamespace,
            Namespaces.PersistenceNamespace,
            Namespaces.PresentationNamespace,
        };

        // Act
        var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Infrastructure_Should_Not_Have_Dependencies() {

        // Arrange
        var assembly = typeof(Infrastructure.IAssemblyReference).Assembly;

        var otherProjects = new[] {
            Namespaces.PersistenceNamespace,
            Namespaces.PresentationNamespace,
        };

        // Act
        var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Persistence_Should_Not_Have_Dependencies() {

        // Arrange
        var assembly = typeof(Persistence.IAssemblyReference).Assembly;

        var otherProjects = new[] {
            Namespaces.InfrastructureNamespace,
            Namespaces.PresentationNamespace,
        };

        // Act
        var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }


    [Fact]
    public void Presentation_Should_Not_Have_Dependencies() {

        // Arrange
        var assembly = typeof(Presentation.IAssemblyReference).Assembly;

        var otherProjects = new[] {
            Namespaces.PersistenceNamespace,
            Namespaces.InfrastructureNamespace,
        };

        // Act
        var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }
}
