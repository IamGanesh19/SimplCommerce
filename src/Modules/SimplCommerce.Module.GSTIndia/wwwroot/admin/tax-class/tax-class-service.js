/*global angular*/
(function () {
    angular
        .module('simplAdmin.gstindia')
        .factory('taxClassService', taxClassService);

    /* @ngInject */
    function taxClassService($http) {
        var service = {
            getTaxClass: getTaxClass,
            createTaxClass: createTaxClass,
            editTaxClass: editTaxClass,
            deleteTaxClass: deleteTaxClass,
            getTaxClasses: getTaxClasses
        };
        return service;

        function getTaxClass(id) {
            return $http.get('api/gst/tax-classes/' + id);
        }

        function getTaxClasses() {
            return $http.get('api/gst/tax-classes');
        }

        function createTaxClass(taxClass) {
            return $http.post('api/gst/tax-classes', taxClass);
        }

        function editTaxClass(taxClass) {
            return $http.put('api/gst/tax-classes/' + taxClass.id, taxClass);
        }

        function deleteTaxClass(taxClass) {
            return $http.delete('api/gst/tax-classes/' + taxClass.id, null);
        }
    }
})();
