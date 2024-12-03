pipeline {
  agent { label 'linux' }  

  tools {
    dotnetsdk 'dotnet_sdk_name'  
  }

  stages {
    stage('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }

    stage('Build') {
      steps {
        sh 'dotnet build --configuration Release --no-restore'
      }
    }

    stage('Test') {
      steps {
        sh 'dotnet test --no-build --no-restore'
      }
    }

    stage('Publish') {
      steps {
        sh 'dotnet publish --configuration Release --output publish --no-build'
      }
    }
  }

  post {
    always {
      echo 'Завершение пайплайна.'
      archiveArtifacts artifacts: 'publish/**/*'
    }
    success {
      echo 'Пайплайн успешно завершен!'
    }
    failure {
      echo 'Ошибка в пайплайне!'
    }
  }
}
