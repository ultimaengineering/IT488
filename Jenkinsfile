pipeline {
  triggers { cron('H H(14-21)   H(1-5)') }
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
          sh 'kanikoexecutor -f Dockerfile --destination=docker.ultimaengineering.iojenkins-masterlatest'
        }
      }
    }
  }
}