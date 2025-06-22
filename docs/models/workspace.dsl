workspace {

  model {
    user = person "User" {
      description "Someone who wants shortens URLs"
    }

    virusTotal = softwareSystem "VirusTotal API" {
      description "External virus scanning service for URLs"
      tags "External"
    }

    database = softwareSystem "Database (SQL)" {
      description "Database for storing URL data"
      tags "External", "Database"
    }

    urlShortenerApi = softwareSystem "UrlShortener" {
      description "Provides an way to shorten URLs and check them with VirusTotal"

      api = container "API Service" {
        technology "ASP.NET Core"
        description "Handles user requests"
      }

      user -> api "Uses"
      api -> virusTotal "Checks URLs via API"
      api -> database "Reads/writes URL data"
    }
  }

  views {
    systemContext urlShortenerApi {
      include *
      autolayout lr
    }

    container urlShortenerApi {
      include *
      autolayout lr
    }

    styles {
      element "External" {
        background #eeeeee
        border dashed
      }

      element "Database" {
        shape cylinder
        background #ffffff
        color #000000
      }
    }
  }

}
