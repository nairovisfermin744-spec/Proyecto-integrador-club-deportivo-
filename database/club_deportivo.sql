CREATE DATABASE IF NOT EXISTS club_deportivo
  CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE club_deportivo;

CREATE TABLE IF NOT EXISTS usuarios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario VARCHAR(50) NOT NULL UNIQUE,
  clave VARCHAR(255) NOT NULL,
  activo BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS socios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  documento VARCHAR(20) NOT NULL UNIQUE,
  nombre VARCHAR(80) NOT NULL,
  apellido VARCHAR(80) NOT NULL,
  telefono VARCHAR(40) NULL,
  email VARCHAR(120) NULL,
  fecha_alta DATE NOT NULL,
  vencimiento_cuota DATE NULL
);

CREATE TABLE IF NOT EXISTS pagos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  socio_id INT NOT NULL,
  fecha_pago DATE NOT NULL,
  monto DECIMAL(12,2) NOT NULL,
  periodo_desde DATE NOT NULL,
  periodo_hasta DATE NOT NULL,
  CONSTRAINT fk_pagos_socios FOREIGN KEY (socio_id) REFERENCES socios(id)
);

INSERT IGNORE INTO usuarios(usuario, clave, activo) VALUES ('admin', 'admin123', TRUE);
