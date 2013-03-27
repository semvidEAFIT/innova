<?php

abstract class Service {

    function __construct() {
        $this->includeFiles();
        echo json_encode($this->callService());
    }
    /**
     * Includes all the files that are needed for the base of the
     * webservice
     */
    function includeFiles() {
        include('../dao/DAO.php');
        include('../helpers/ArrayHelper.php');
        include('../helpers/ErrorHelper.php');
        include('../control/Controller.php');
        $this->includeSpecificFiles();
    }

    /**
     * Method that allows you to include the files you only need for the specific
     * service
     */
    abstract function includeSpecificFiles();

    /**
     * Method that checks the parameters needed and calls the specific service
     * @return array a PHP arrray to be encoded as a JSON Object
     */
    abstract function callService();

    // <editor-fold defaultstate="collapsed" desc="Helper Service Methods">

    /**
     * Returns true if all of the variables exist in the GET array
     * @return boolean
     */
    public static function checkParamsPOST() {
        $ok = true;
        $argCount = func_num_args();
        for ($i = 0; $i < $argCount; $i++) {
            if (!Service::checkParamPOST(func_get_arg($i))) {
                $ok = false;
                break;
            }
        }
        return $ok;
    }

    /**
     * Checks the variable exists in the POST array and its not an empty variable
     * @param type $param - key of the POST array
     * @return valid - returns if the variable is valid or not
     */
    public static function checkParamPOST($param) {
        if (!isset($_POST[$param]))
            return FALSE;
        if (empty($_POST[$param]))
            return FALSE;

        return TRUE;
    }

    /**
     * Returns true if all of the variables exist in the GET array
     * @return boolean
     */
    public static function checkParamsGET() {
        $ok = true;
        $argCount = func_num_args();
        for ($i = 0; $i < $argCount; $i++) {
            if (!Service::checkParamGET(func_get_arg($i))) {
                $ok = false;
                break;
            }
        }
        return $ok;
    }

    /**
     * Checks the variable exists in the GET array and its not an empty variable
     * @param type $param - key of the POST array
     * @return valid - returns if the variable is valid or not
     */
    public static function checkParamGET($param) {
        if (!isset($_GET[$param]))
            return FALSE;
        if (empty($_GET[$param]))
            return FALSE;

        return TRUE;
    }

    // </editor-fold>
}

?>
