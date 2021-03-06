﻿(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['apiService',
        '$scope',
        'notificationService',
        '$state',
        'commonService',
        '$stateParams'];

    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.product = {
        };

        $scope.UpdateProduct = UpdateProduct;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.getProductCategories = getProductCategories;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseMoreImage = ChooseMoreImage;

        $scope.moreImages = [];

        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }

        function ChooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        function getProductCategories() {
            apiService.get('/api/productcategory/getallparent', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                notificationService.displayError('Không tải được danh mục sản phẩm');
            });
        }


        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }


        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' Đã được cập nhật');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        }

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                if (JSON.parse($scope.product.MoreImages) != null)
                    $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getProductCategories();
        loadProductDetail();
    }

})(angular.module('linhnhishop.products'));