myApp.controller('CreateCollectionController', ['$scope', function ($scope) {
    $scope.collection = {
        title: '',
        description: '',
        image: '',
        link: ''
    };

    $scope.saveCollection = function () {
        // Use Axios to make a POST request to your API endpoint
        axios.post('/CMS/saveCollection', $scope.collection)
            .then(function (response)
            {
                if (response.data.success)
                {
                   // alert('collection created successfully!');
                    alert(response.data.message);
                    window.location.href = '/Cms/ListPage';
                }
                else {
                    alert('Failed to create collection. ');
                }
            })
            .catch(function (error) {
                console.error('Error creating collection:', error);
                alert('Failed to create collection. Please try again.');
            });
    };
}]);

myApp.controller('ListPageController', ['$scope', '$http', function ($scope, $http) {
    $scope.initList = function () {
        // Function to fetch collections from the backend
       // $scope.getAllCollections = function () {
            $http.get('/CMS/ListPage1')
                .then(function (response) {
                    // Success callback
                    if (response.data) {
                        $scope.collectionList = response.data;
                    }

                }, function (error) {
                    // Error callback
                    console.error('Error fetching collections:', error);
                });
        };
        // }
    // Call the function to fetch collections when the controller loads
}]);