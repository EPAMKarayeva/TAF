pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                script {
                    bat "dotnet build"
                }
            }
        }
        stage('Test') {
            steps {
                script {
                    bat "dotnet test --logger trx"
                }
            }
        }
        stage('Publish Test Results') {
            steps {
                publishTestResults "**/*.trx"
            }
        }
    }
}
