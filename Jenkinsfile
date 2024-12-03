pipeline {
  agent { label "build && windows" }
  stages {
    stage('Clean Workspace'){
      steps {
        cleanWs()
      }
    }
    
    stage('Checkout'){
      steps {
        checkout([$class: 'GitSCM', 
        branches: [[name: '*/master']], 
        doGenerateSubmoduleConfigurations: false, 
        extensions: [], 
        submoduleCfg: [], 
        userRemoteConfigs: [[url: 'https://github.com/EPAMKarayeva/TAF.git']]])

      }
    }
    
    stage('Nuget Restore') {
      steps {
        bat label: 'Nuget Restore', 
        script: '''
          nuget restore "PrimeDotnet\\prime-dotnet.sln"
          echo "Nuget Done Starting Msbuild *************"
        ''' 
      }
    }

    stage('Build') {
      steps {
        script {
          tool name: 'msbuild_2017', type: 'msbuild'
          bat "\"${tool 'msbuild_2017'}\"\\msbuild.exe PrimeDotnet\\prime-dotnet.sln /P:DeployOnBuild=True /p:AllowUntrustedCertificate=True /p:MSDeployServiceUrl=<IP or Hostname of IIS server> /P:DeployIISAppPath=\"Default Web Site/PrimeDotnet\""
        }
      }
    }

    stage('UnitTest') {
      steps {
        script {
          bat label: 'Unit Test using Dotnet CLI',
        script: '''
          dotnet.exe test .\\PrimeDotnet\\
        '''
        }
      }
    }
