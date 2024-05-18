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
                    window.location.href = '/Cms/Index';
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



myApp.controller('EditPageController', ['$scope', '$http', function ($scope, $http) {
    $scope.initEdit = function (model) {
        // Function to fetch collections from the backend
        // $scope.getAllCollections = function () {
        //$http.get('/CMS/EditPage')
        //    .then(function (response) {
        //        // Success callback
        //        if (response.data) {
        //            $scope.collectionList = response.data;
        //        }

        //    }, function (error) {
        //        // Error callback
        //        console.error('Error fetching collections:', error);
        //    });
        $scope.collection = model;
    };
    // }
    // Call the function to fetch collections when the controller loads

    $scope.UpdateCollection = function () {
        if ($scope.collectionForm.$valid) {
            var data = {
                id: $scope.collection.id,
                title: $scope.collection.title,
                description: $scope.collection.description,
                image: $scope.collection.image,
                link: $scope.collection.link,
            };

            // Make POST request using Axios
            axios.post('/Cms/updateCollection', data, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(function (response) {
                    // Handle success response
                   /* if (response.data) {*/
                    alert(response.data.Message);
                        window.location.href = '/CMS/Index';
                    //} else {
                    //    alert(response.data.Message);
                    //}
                })
                .catch(function (error) {
                    // Handle error
                    console.error(error);
                });
        } else {
            // Form is invalid, display error messages
            $scope.productForm.$submitted = true;
        }

    };

}]);

myApp.controller('ListPageController', ['$scope', '$http', function ($scope, $http) {
    $scope.initList = function () {
        // Function to fetch collections from the backend
       // $scope.getAllCollections = function () {
        $http.get('/CMS/GetCollections')
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

