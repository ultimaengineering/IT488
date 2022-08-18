pipeline {
  agent {
    kubernetes {
      yamlFile 'KubernetesBuilder.yaml'
    }
  }
  stages {
    stage('Release') {
      steps {
	checkout scm
        container('kaniko') {
          sh 'ulimit -n 10000'
          sh '/kaniko/executor -f Dockerfile --destination=docker.ultimaengineering.io/it488-project:${BRANCH_NAME}-${BUILD_NUMBER}'
        }
      }
    }
  }
}
