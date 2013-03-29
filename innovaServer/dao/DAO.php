<?php

include ('../dao/MysqlDBC.php');

class DAO {

    private $dbEngine;

    function __construct() {
        $this->dbEngine = MysqlDBC::getInstance();
    }

     // <editor-fold defaultstate="collapsed" desc="Player Methods">
    function getPlayerByDocument($document){
        $document = $this->dbEngine->clearVar($document);
        
        $result = $this->dbEngine->getResult(
                "SELECT * FROM players WHERE document = '$document' LIMIT 1");
        $row = $result->fetch_object();
        if($row != null){
            return Player::getPlayerObject($row);
        }else{
            return getErrorArray('01', "No existe un jugador con el documento '$document'");
        }
    }
    
    function updatePlayer($Player){
        $id = $Player->getId();
        $playCount = $Player->getPlayCount();
        $score = $Player->getScore();
        $lastDate = $Player->getLastDate();
        
        return $this->dbEngine->update(
                "UPDATE players SET `score`='$score', `playCount`='$playCount', `lastDate`='$lastDate' WHERE `id`='$id'");
    }
    
    function getPlayers(){
        $result = $this->dbEngine->getResult("SELECT * FROM players");
        $PlayersArray = array();
        while($row = $result->fetch_object()){
            $Player = Player::getPlayerObject($row);
            array_push($PlayersArray, $Player);
        }
        return $PlayersArray;
    }
    
    function getTopPlayers($topLength){
        $topLength = $this->dbEngine->cleanVar($topLength);
        $result = $this->dbEngine->getResult(
                "SELECT * FROM players ORDER BY score DESC LIMIT $topLength");
        $PlayersArray = array();
        while($row = $result->fetch_object()){
            $Player = Player::getPlayerObject($row);
            array_push($PlayersArray, $Player);
        }
        return $PlayersArray;
    }
    
    function createPlayer($Player){
        $id = 0;
        $document = $Player->getDocument();
        $name = $Player->getName();
        $lastNames = $Player->getLastNames();
        $email = $Player->getEmail();
        $institution = $Player->getInstitution();
        $score = $Player->getScore();
        $playCount = $Player->getPlayCount();
        $lastDate = $Player->getLastDate();
        
        return $this->dbEngine->insert(
                "INSERT INTO players (id, document, name, lastNames, email, institution, score, playCount, lastDate)
                    VALUES ($id, '$document', '$name', '$lastNames', '$email', '$institution', $score, $playCount, '$lastDate')");
    }
    // </editor-fold>
}

?>
