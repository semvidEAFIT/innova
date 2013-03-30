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
    private $lastName;
    private $email;
    private $institution;
    private $score;
    private $playCount;
    private $lastDate;
    
    function __construct($id, $document, $name, $lastName, $email, $institution, $score, $playCount, $lastDate) {
        $this->id = $id;
        $this->document = str_replace(str_split("./-\\<> "), "", $document);
        $this->name = $name;
        $this->lastName = $lastName;
        $this->email = $email;
        $this->institution = $institution;
        $this->score = $score;
        $this->playCount = $playCount;
        $this->lastDate = $lastDate;
    }

    public function toArray(){
        return array(
                'id' => $this->getId(),
                'document' => $this->getDocument(),
                'name' => $this->getName(),
                'lastName' => $this->getLastName(),
                'email' => $this->getEmail(),
                'institution' => $this->getInstitution(),
                'score' => $this->getScore(),
                'playCount' => $this->playCount,
                'lastDate' => $this->getLastDate()
        );
    }
    
    static public function getPlayerObject($object) {
        if (property_exists($object, "Player")) {
            $object = $object->Player;
        }
        return new Player(
               $object->id, 
                $object->document, 
                $object->name, 
                $object->lastName, 
                $object->email, 
                $object->institution, 
                $object->score, 
                $object->playCount, 
                $object->lastDate
        );
    }
    
    // <editor-fold defaultstate="collapsed" desc="Gets y Sets">

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

    public function getLastName() {
        return $this->lastName;
    }

    public function setLastName($lastName) {
        $this->lastName = $lastName;
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
    
    public function getScore() {
        return $this->score;
    }

    public function setScore($score) {
        $this->score = $score;
    }

    public function getPlayCount() {
        return $this->playCount;
    }

    public function setPlayCount($playCount) {
        $this->playCount = $playCount;
    }
        
    public function getLastDate() {
        return $this->lastDate;
    }

    public function setLastDate($lastDate) {
        $this->lastDate = $lastDate;
    }

    // </editor-fold>
}

?>
