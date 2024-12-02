pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                script {
                    sh "dotnet build"
                }
            }
        }
        stage('Test') {
            steps {
                script {
                    sh "dotnet test --logger trx"
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
