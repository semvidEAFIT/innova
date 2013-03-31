<?php
/**
 * Description of ArrayHelper
 * @author Rodrigo
 */

class ArrayHelper {

    public static function toArray($arrayConvertible) {
        if (!$arrayConvertible)
            return array();
        $jsonArray = array();
        foreach ($arrayConvertible as &$object){
            array_push($jsonArray, ArrayHelper::toArrayObject($object));
        }
        return $jsonArray;
    }

    public static function toArrayObject($object) {
        if ($object) {
            if (method_exists($object, 'toArray')) {
                return $object->toArray();
            } else {
                return array("NULL");
            }
        } else {
            return array("NULL");
        }
    }

}

?>
