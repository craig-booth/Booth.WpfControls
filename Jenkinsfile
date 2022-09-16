pipeline {
    agent { docker 'mcr.microsoft.com/dotnet/sdk:6.0-alpine' }
	
	environment {
		PROJECT      = './Booth.WpfControls/Booth.WpfControls.csproj'
        TEST_PROJECT = './Booth.WpfControls.Tests/Booth.WpfControls.Tests.csproj'

		NUGET_KEY = credentials('nuget')
    }

    stages {
		stage('Build') {
			steps {
				sh "dotnet build ${PROJECT} --configuration Release"
            }
		}
	    stage('Test') {
			steps {
				sh "dotnet test ${TEST_PROJECT} --configuration Release --logger trx --results-directory ./testresults"
            }
			post {
				always {
					xunit (
						thresholds: [ skipped(failureThreshold: '0'), failed(failureThreshold: '0') ],
						tools: [ MSTest(pattern: 'testresults/*.trx') ]
						)
				}
			}
        }
        stage('Deploy') {
			steps {
				sh "dotnet pack ${PROJECT} --configuration Release --output ./deploy"
				sh "dotnet nuget push ./deploy/*.nupkg -k ${NUGET_KEY} -s https://api.nuget.org/v3/index.json"
            }
		}
    }
	
	post {
		success {
			cleanWs()
		}
	}
}