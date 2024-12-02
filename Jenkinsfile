pipeline {
    agent any
    
    tools {
        dotnetsdk 'default-dotnetsdk'
    }
     
    stages {
        stage('Validation') {
            steps {
                script {
                    sh 'dotnet --version'
                }
            }
        }
        stage('Restore') {
            steps {
                script {
                    sh 'dotnet restore'
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    sh 'dotnet build'
                }
            }
        }
        stage('Test') {
            steps {
                script {
                    sh 'dotnet test --logger trx'
                }
            }
        }
        stage('Publish Test Results') {
            steps {
                publishTestResults '**/*.trx'
            }
        }
    }
}
