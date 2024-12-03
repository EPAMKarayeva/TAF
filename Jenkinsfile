pipeline {
  agent { label 'windows' }

  tools {
    msbuild 'msbuild_tool_name'
    dotnetsdk 'dotnet_sdk_name' 
  }

  stages {
    stage('Restore') {
      steps {
        bat 'dotnet restore'
      }
    }

    stage('Build') {
      steps {
        bat 'dotnet build --configuration Release'
      }
    }

    stage('Test') {
      steps {
        bat 'dotnet test'
      }
    }

    stage('Publish') {
      steps {
        bat 'dotnet publish --configuration Release --output publish'
      }
    }
  }
}
