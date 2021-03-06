﻿
(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['apiService',
        '$scope',
        'notificationService',
        '$state',
        'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreateDate: new Date(),
            Status: true
        }

        $scope.AddProduct = AddProduct;
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
                });
            }
            finder.popup();
        }

        function ChooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
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


        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' Đã được thêm mới');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }

        $scope.getProductCategories();
    }

})(angular.module('linhnhishop.products'));