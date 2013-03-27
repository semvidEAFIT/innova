<?php

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 * Description of Player
 *
 * @author Camilo
 */
class Player {
    
    private $id;
    private $document;
    private $name;
    private $email;
    private $institution;
    private $lastDate;
    
    function __construct($id, $document, $name, $email, $institution, $lastDate) {
        $this->id = $id;
        $this->document = $document;
        $this->name = $name;
        $this->email = $email;
        $this->institution = $institution;
        $this->lastDate = $lastDate;
    }

    public function toArray(){
        return array(
                'id' => $this->getId(),
                'document' => $this->getDocument(),
                'name' => $this->getName(),
                'email' => $this->getEmail(),
                'institution' => $this->getInstitution(),
                'lastDate' => $this->getLastDate()
        );
    }
    
    static public function getPlayerObject($object) {
        if (property_exists($object, "Player")) {
            $object = $object->Player;
        }
        return new Player(
               $object->id, $object->document, $object->name, $object->email, $object->institution, $object->lastDate
        );
    }
    
    public function getId() {
        return $this->id;
    }

    public function setId($id) {
        $this->id = $id;
    }

    public function getDocument() {
        return $this->document;
    }

    public function setDocument($document) {
        $this->document = $document;
    }

    public function getName() {
        return $this->name;
    }

    public function setName($name) {
        $this->name = $name;
    }

    public function getEmail() {
        return $this->email;
    }

    public function setEmail($email) {
        $this->email = $email;
    }

    public function getInstitution() {
        return $this->institution;
    }

    public function setInstitution($institution) {
        $this->institution = $institution;
    }
    
    public function getLastDate() {
        return $this->lastDate;
    }

    public function setLastDate($lastDate) {
        $this->lastDate = $lastDate;
    }



}

?>
