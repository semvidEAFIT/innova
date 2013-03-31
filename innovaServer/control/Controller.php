<?php

/**
 * 
 */
class Controller {

    private $dao;
    // <editor-fold defaultstate="collapsed" desc="Singleton Section">
    private static $instance;

    public static function getInstance() {
        if (!self::$instance instanceof self) {
            self::$instance = new self;
        }
        return self::$instance;
    }

    // </editor-fold>

    function __construct() {
        $this->dao = new DAO();
    }

    // <editor-fold defaultstate="collapsed" desc="Player Methods">

    public function getTopPlayers($topLength){
        return $this->dao->getTopPlayers($topLength);
    }
    
    public function registerPlayer($player){
        $document = $player->getDocument();
        $previousRegister = $this->dao->getPlayerByDocument($document);
        $date = date('Y-m-d H:i:s');
        $player->setLastDate($date);
        if($previousRegister == null){ //El jugador no existe en la base de datos
            return $this->dao->createPlayer($player);
        }else{
            $player->setId($previousRegister->getId());
            $player->setPlayCount($previousRegister->getPlayCount() + 1);
            if($previousRegister->getScore() > $player->getScore()){
                $player->setScore($previousRegister->getScore());
            }
            return $this->dao->updatePlayer($player);
        }
    }
    
    public function getPlayers(){
        return $this->dao->getPlayers();
    }

    public function getPlayerRanking($document){
        $register = $this->dao->getPlayerByDocument($document);
        if($register != null){
            return $this->dao->getPlayerRanking($register);
        }else{
            return getErrorArray("03", "El jugador no esta en el ranking");
        }
    }
    // </editor-fold>
}

?>
