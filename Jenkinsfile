pipeline {
    agent any

    stages {
        stage('Trigger Check') {
            steps {
                echo '✅ Jenkins pipeline triggered successfully!'
                sh 'date'
                sh 'echo "Repository checkout and pipeline execution OK"'
            }
        }
    }
}

