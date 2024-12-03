pipeline {
  agent { label 'linux' }  // Используйте метку, соответствующую Linux агентам

  tools {
    dotnet 'dotnet_sdk_name'  // Уточните название .NET SDK, настроенное в Jenkins
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
    }
    success {
      echo 'Пайплайн успешно завершен!'
    }
    failure {
      echo 'Ошибка в пайплайне!'
      // Отправка уведомления, запись в лог, etc.
    }
    // Шаг для архивации артефактов
    always {
      archiveArtifacts artifacts: 'publish/**/*'
    }
  }
}
