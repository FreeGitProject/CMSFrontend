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
            .then(function (response) {
                alert('Collection created successfully!');
                // Optionally, redirect to another page or perform additional actions
            })
            .catch(function (error) {
                console.error('Error creating collection:', error);
                alert('Failed to create collection. Please try again.');
            });
    };
}]);