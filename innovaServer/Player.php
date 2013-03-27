<?php

/**
 * Description of User
 *
 * @author Camilo
 */
class Player {
    
    //<editor-fold defaultstate="collapsed" desc="Private Variables">
    private $telefono;
    private $correo;
    private $nombre;
    private $id;
    //</editor-fold>
    
    public function __construct($id, $nombre, $correo, $telefono) {
        $this -> $id = $id;
        $this -> $nombre = $nombre; 
        $this -> $correo = $correo;
        $this -> $telefono = $telefono;
    }
   
    //<editor-fold defaultstate="collapsed" desc="Getters and Setters">
    public function getTelefono() {
        return $this->telefono;
    }

    public function setTelefono($telefono) {
        $this->telefono = $telefono;
    }

    public function getCorreo() {
        return $this->correo;
    }

    public function setCorreo($correo) {
        $this->correo = $correo;
    }

    public function getNombre() {
        return $this->nombre;
    }

    public function setNombre($nombre) {
        $this->nombre = $nombre;
    }
    //</editor-fold>

    
}

?>
