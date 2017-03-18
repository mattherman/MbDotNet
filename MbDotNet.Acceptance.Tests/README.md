# Running Acceptance tests
- Please run an instance of mountebank on the default localhost and port to run these tests against
- Mountebank should be run with the --mock option enabled in order for the imposter request field to be populated. See http://www.mbtest.org/docs/api/overview#get-imposter.
- If you would prefer to run mountebank via docker, please execute the following command from the root directory:
```docker-compose up```