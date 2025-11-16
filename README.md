# <div align="center"> ROCK & MOLE </div>
<div align="center">GAME DESIGN DOCUMENT - v0.2.0</div>

<br><br><br><br>

**Desarrollado por:**<br>
Banana Slip

<br><br><br>

# 1. INTRODUCCIÓN
   
## 1.1. DESCRIPCIÓN DEL CONTENIDO

*Rock & Mole* es un juego de aventura y acción con vista isométrica en 3D, ambientado en un pueblo subterráneo habitado por animales excavadores. El jugador controla a un joven topo que se adentra en una mina compuesta por salas diseñadas manualmente que se reorganizan aleatoriamente en cada run. A lo largo de la exploración, combate enemigos, encuentra eventos especiales y recolecta recursos que pueden venderse en la tienda para conseguir monedas o usarse para mejorar las armas y armaduras.

## 1.2. CARACTERÍSTICAS PRINCIPALES

**Exploración modular y rejugable:** La mina está compuesta por salas diseñadas manualmente que se reorganizan aleatoriamente en cada partida. 

**Combate estratégico y ágil:** El jugador combate con un pico en enfrentamientos cuerpo a cuerpo contra enemigos con patrones definidos, lo que obliga a pensar tácticamente. La dificultad aumenta con la profundidad.

**Mejora persistente de equipo:** Aunque cada run reinicia la mina, el progreso de armas y armaduras se conservan. Los materiales recolectados permiten mejorar el pico, la vestimenta y el casco. Esta progresión persistente motiva al jugador a seguir explorando.

**Eventos especiales y salas únicas:** Durante la exploración el jugador puede encontrar eventos especiales con salas únicas: cofres raros, zonas oscuras, rescates de NPCs o campamentos de descanso. Estos eventos aportan dinamismo y rejugabilidad.

**Gestión y comercio en el pueblo:** Fuera de la mina el jugador interactúa con NPCs que permiten vender objetos y mejorar equipo. El pueblo funciona como centro de operaciones, ofreciendo una pausa entre runs y reforzando el vínculo con la historia.

**Diseño escalable y expansible:** El juego está diseñado para crecer: nuevos biomas, enemigos, materiales y eventos pueden añadirse fácilmente gracias a una arquitectura modular. Esto permite actualizaciones constantes y contenido adicional sin comprometer la estabilidad del sistema base.

## 1.3. GÉNERO

*Rock & Mole* se encuentra dentro del género de aventura y acción, con presencia de mecánicas de gestión. El jugador no sólo explora y combate, sino que también administra su propia tienda, decide precios y mejora su equipo, añadiendo una capa de estrategia al juego. La estructura del juego cumple con los pilares de un roguelite, en el que cada partida presenta una mina distinta gracias a la reorganización aleatoria de salas, pero con una progresión persistente que permite conservar mejoras en armas y armaduras entre runs.

## 1.4. PLATAFORMAS

*Rock & Mole* se desarrolla como un juego web, accesible desde navegadores en PC y dispositivos móviles. En ordenadores, los jugadores pueden utilizar teclado y ratón, o bien jugar con mando y en móviles, sólo es posible conectar un mando externo para jugar.

## 1.5. ESTILO VISUAL

El estilo visual del juego combina una estética low- poly en 3D con vista isométrica y una ambientación cálida y subterránea. La dirección artística se inspira en títulos como *Moonlighter 2*, especialmente en el uso de colores suaves e iluminación ambiental, con texturas planas y simples.

El protagonista y los NPCs están basados en animales que habitan bajo tierra, como topos o musarañas. Los enemigos representan una fusión entre criaturas animales (corrompidas) y elementos fantásticos.

## 1.6. PROPÓSITO Y PÚBLICO OBJETIVO

El propósito principal del juego es ofrecer una experiencia que combine exploración, combate y gestión económica dentro de un mundo subterráneo. A través de mecánicas roguelite y progresión persistente, se busca mantener la rejugabilidad. 

El juego está dirigido a un público que abarca principalmente jóvenes y adultos entre 13 y 35 años, con interés en juegos de aventura, acción y gestión ligera. Se prioriza la experiencia que pueda disfrutarse tanto en sesiones cortas como en exploraciones más largas, adaptándose a las plataformas disponibles. 

---

  
# 2. MONETIZACIÓN Y MODELO DE NEGOCIO

El modelo de monetización de *Rock & Mole* se basa en un sistema Buy to Play, es decir, un pago único por el juego base. Esta decisión busca ofrecer una experiencia completa y accesible para el jugador.

Para mantener la viabilidad económica del proyecto a largo plazo, se implementará además una estrategia de monetización mediante DLCs (contenido descargable). Estos se lanzarán periódicamente una vez que la actividad de los jugadores en el juego base comience a disminuir. Cada DLC incluirá contenido significativo como nuevos niveles de la mina, enemigos, materiales recogibles adicionales y mejoras para el equipo del jugador.

Entre los lanzamientos de DLCs, se realizará la publicación de actualizaciones menores gratuitas que mantengan el interés de los jugadores sin requerir grandes esfuerzos de desarrollo. Estas actualizaciones incluirán elementos como una nueva armadura, un enemigo adicional o pequeños ajustes. El objetivo es mantener viva la experiencia de juego sin sobrecargar al equipo de desarrollo ni que suponga un problema económico al jugador.

---

  
# 3. HISTORIA

El protagonista, un joven topo, vive en un pueblo subterráneo que depende de los minerales de una antigua mina. Tras un misterioso cierre, la mina se volvió peligrosa y oscura. Para salvar a su comunidad y descubrir qué ocurrió, el topo decide adentrarse en sus profundidades, enfrentando criaturas corrompidas y buscando recuperar los recursos perdidos.

---

  
# 4. MECÁNICAS
## 4.1. JUGABILIDAD

**Estructura de las runs:** Cada partida se desarrolla en una mina subterránea compuesta por salas conectadas. En cada run, el jugador desciende hacia niveles más profundos, enfrentándose a enemigos, recolectando materiales y descubriendo eventos especiales. Las salas están diseñadas manualmente pero se reorganizan de forma aleatoria en cada intento.

Las salas pueden contener enemigos, recursos mineros, cofres o eventos únicos. El objetivo es llegar lo más lejos posible, recolectar materiales valiosos y regresar al pueblo para mejorar el equipo. La partida termina si el jugador es derrotado o si logra llegar al final del nivel y vencer al jefe.

En la run las salas son predefinidas, con cierta aleatoriedad en la generación de enemigos y rocas. Hay una pool de salas en el level manager, lo que hace que cada vez que entre el jugador a la escena sea una estructura de salas que se pueda repetir pero la organización cantidad y tipo de enemigos pueda variar.

**Dificultad progresiva:** La dificultad aumenta conforme se desciende en la mina. Los enemigos se vuelven más agresivos, aparecen nuevos tipos de enemigos y los recursos se vuelven más escasos. La intensidad de cada run está determinada por la profundidad, el tipo de enemigos y la combinación de salas.

**Combate:** El sistema de combate es en tiempo real, con un enfoque táctico. El jugador ataca con su pico, puede esquivar con un dash y debe aprender los patrones de los enemigos para sobrevivir. Cada criatura tiene comportamientos definidos.

**Recursos y minería:** Los materiales recogidos en la mina tienen un doble propósito: pueden venderse en la tienda gestionada por el jugador para obtener dinero o utilizarse en la herrería y en la tienda de ropa para mejorar el pico, la armadura y el casco. La gestión de estos recursos es clave para progresar en el juego.

**Gestión de la tienda:** Al regresar al pueblo, el jugador puede acceder a su propia tienda, donde gestionar la venta de materiales. Puede organizar los objetos que ha recolectado y establecer el precio de cada uno. Los NPCs del pueblo visitan la tienda y, según el precio, la rareza y su interés, pueden comprar o ignorar los productos. Vender a buen precio permite obtener monedas para invertir en mejoras, pero si los precios son demasiado altos, los aldeanos no comprarán. La tienda evoluciona con el progreso del jugador.

**Progresión del jugador:** Aunque cada run comienza desde cero, el jugador mantiene las mejoras de equipo obtenidas. Esto permite avanzar más en cada intento, enfrentarse a enemigos más fuertes y acceder a salas más complejas.

**Planificación y estrategia:** Antes de cada run, el jugador debe decidir si quiere mantener el equipo actual o invertir en mejoras. No hay selección de habilidades como tal, pero el tipo de armadura, casco y pico equipado influye directamente en el rendimiento.

## 4.2. CONTROLES

El juego está diseñado para jugarse tanto en PC como en dispositivos móviles. En ordenador, el jugador puede utilizar teclado y ratón o con mando de Xbox, PlayStation y Nintendo. En móvil, el juego requiere el uso de mando conectado, ya que no está optimizado para controles táctiles.

| Acción | Teclado y ratón (PC) | Mando Xbox | Mando PlayStation | Mando Nintendo |
| :----: | :------------------: | :--------: | :---------------: | :-------------: |
| Moverse | Teclas WASD         | Joystick izquierdo | Joystick izquierdo | Joystick izquierdo |
| Atacar | Click izquierdo | RT (gatillo derecho) | R2 (gatillo derecho) | ZR (gatillo derecho) |
| Dash | Shift | LT (gatillo izquierdo) | L2 (gatillo izquierdo) | ZL (gatillo izquierdo) |
| Interactuar | E | X | □ | Y |  

<div align="center"><p><i>Tabla 1: Controles de personaje.</i></p></div>

<br>  

| Interfaz | Teclado y ratón (PC) | Mando Xbox | Mando PlayStation | Mando Nintendo |
| :----: | :------------------: | :--------: | :---------------: | :-------------: |
| Pausa | Esc         | ≡ | Options | + |
| Inventario | Tab | Y | △ | X |
| Aceptar | Clic | A | X | A |
| Diálogos | Espacio/Clic | A | X | A |  

<div align="center"><p><i>Tabla 2: Controles de interfaces.</i></p></div>

<br>

## 4.3. PERSONAJES

**Protagonista:**

* **Nombre:** Otto.
* **Sexo**: Masculino.
* **Especie:** Topo.
* **Edad**: 22 años.
* **Personalidad:** Otto es un personaje tranquilo y sereno, capaz de mantener la calma incluso en situaciones extremas. Esta actitud le permite tomar decisiones con cabeza fría, aunque a veces lo lleva a asumir riesgos que otros evitarían. Tiene una curiosidad natural y un fuerte sentido de responsabilidad hacia su comunidad, lo que lo impulsa a seguir adelante pese al peligro.
* **Aspecto físico:** De estatura y complexión media, Otto viste con ropa de trabajo desgastada por el uso: una camiseta y un peto cubiertos de tierra y marcas de desgaste. Lleva siempre un casco de minero naranja con luz frontal, esencial para explorar zonas oscuras. Su mochila grande y resistente le permite transportar los objetos que encuentra en sus expediciones por la mina.
* **Rol:** Otto es el personaje principal controlado por el jugador. Su rol combina exploración y comercio: recorre las profundidades de la mina en busca de recursos, enfrentando peligros y recolectando objetos que luego puede intercambiar para ayudar a su comunidad.
* **Trasfondo:** Vive en un pueblo subterráneo que depende de los minerales de una antigua mina. Tras un misterioso cierre, la mina se volvió peligrosa y oscura. Para salvar a su comunidad y descubrir qué ocurrió, Otto decide adentrarse en sus profundidades, enfrentando criaturas corrompidas y buscando recuperar los recursos perdidos.

**Vendedor:**

* **Nombre:** Telmo.
* **Sexo**: Masculino.
* **Especie:** Topo.
* **Edad:** 28 años.
* **Personalidad:** Telmo es extrovertido, hablador y siempre está al tanto de las últimas tendencias, incluso bajo tierra. Tiene un gran sentido del humor y una actitud comercial muy marcada: siempre intenta convencer al jugador de que necesita una nueva prenda. Aun así, se preocupa por el bienestar del pueblo y colabora activamente con los exploradores.
* **Aspecto físico:** Telmo viste con ropa llamativa y colorida. Su tienda está decorada con telas, maniquíes y luces cálidas que contrastan con el resto del pueblo.
* **Rol:** Compra objetos del jugador y vende armaduras y vestimentas. Telmo regenta la tienda de ropa del pueblo. Aunque su local parece especializado en moda, es el lugar donde el jugador puede vender los materiales recogidos en la mina y adquirir nuevas piezas de equipo.
* **Trasfondo**: Antes del cierre de la mina, Telmo se dedicaba exclusivamente a la moda subterránea. Sin embargo, con la crisis de recursos, adaptó su negocio para ayudar a los exploradores, convirtiendo su tienda en un punto clave para el intercambio de materiales y la mejora del equipo.

**Herrero:**

* **Nombre:** Bruno.
* **Sexo**: Masculino.
* **Especie:** Topo.
* **Edad:** 45 años.
* **Personalidad:** Bruno es serio, meticuloso y reservado. Habla poco, pero cuando lo hace, sus palabras son precisas. Tiene un fuerte sentido del deber y se toma muy en serio su trabajo como herrero. Aunque no lo demuestre abiertamente, se preocupa por la seguridad del jugador y del pueblo.
* **Aspecto físico:** De complexión robusta, Bruno siempre está cubierto de hollín. Lleva un mandil de cuero y guantes gruesos. Su taller está lleno de herramientas, yunques y brasas encendidas, con un ambiente cálido y ruidoso.
* **Rol:** Mejora armas. Bruno es el herrero del pueblo. Se encarga de reforzar el pico, la armadura y el casco del jugador utilizando los materiales obtenidos en la mina. Su taller es el corazón del progreso mecánico del juego.
* **Trasfondo:** Bruno proviene de una larga tradición de herreros subterráneos. Desde joven ha trabajado el metal, y su habilidad para forjar herramientas resistentes lo convirtió en una figura clave del pueblo. Con la mina cerrada, su papel se volvió aún más importante, ya que sin su ayuda, los exploradores no podrían mejorar su equipo ni sobrevivir a los peligros del subsuelo.

**Aldeanos:**

* **Especie:** Variadas (musarañas, ratones, topos, tejones, etc).
* **Personalidad:** Los aldeanos son diversos en carácter y actitud. Algunos son curiosos y entusiastas, otros más temerosos o escépticos. En conjunto, representan el alma del pueblo: una comunidad unida que observa con atención los avances del jugador y reacciona a sus logros o fracasos.
* **Rol:** Ambientación, dan consejos, reaccionan al progreso del jugador. Los aldeanos aportan vida al pueblo. Algunos ofrecen consejos útiles, otros comentan los avances del jugador.
* **Trasfondo:** Los aldeanos han vivido durante generaciones en el pueblo subterráneo, dependiendo de la mina como fuente de recursos. El cierre repentino los dejó en una situación crítica, pero mantienen la esperanza gracias a los esfuerzos del jugador. Su presencia constante refuerza la sensación de comunidad y da contexto emocional al progreso del juego.

<br>

  
## 4.4. ENEMIGOS
### 4.4.1. GÓLEMS

* **Tipo:** Enemigo estándar.
* **Comportamiento:** Cuando el jugador entra en su área de detección, el gólem lo persigue y ataca.
* **Características:** daño y velocidad moderados. Su presencia es común en las salas de combate.
* **Función:** Introduce al jugador en el sistema de combate básico y sirve como enemigo recurrente.

**Gólem pequeño:** 

* **Tipo:** Enemigo menor.
* **Trasfondo:** Estos pequeños enemigos habitan en la mina como extensiones fragmentadas del gólem de roca. No tienen voluntad propia ni inteligencia, pero actúan por instinto defensivo. Su origen suele estar ligado a la división de un gólem mayor tras ser derrotado, aunque también pueden aparecer por sí solos en zonas corrompidas.
* **Características:** Los fragmentos de gólem tienen menor daño y velocidad que otros enemigos, pero suelen aparecer en grupo, lo que aumenta la presión sobre el jugador. Su tamaño reducido y baja resistencia los convierte en blancos fáciles, aunque su número puede complicar el combate si no se gestionan bien. Su aspecto recuerda a pequeñas criaturas de piedra, con formas irregulares que conservan parte de la textura del gólem original.
* **Comportamiento:** Persigue al jugador si entra en su área, pero con menor alcance.
* **Función:** Añade presión en combate por número. Su baja resistencia permite al jugador practicar esquivas y ataques rápidos.

**Gólem grande:**

* **Tipo:** Enemigo estándar.
* **Trasfondo:**  El gólem de roca habita en las profundidades de la mina desde tiempos antiguos. Su existencia está ligada a la protección del entorno subterráneo. No tiene conciencia ni emociones: su único propósito es defender la mina de cualquier intruso. Con el cierre misterioso de la mina, su comportamiento se ha vuelto más agresivo, atacando a todo aquel que se acerque.
* **Características:**  Tiene un daño y una velocidad moderados, lo que lo convierte en una amenaza constante pero manejable para el jugador. Su gran tamaño y cuerpo formado por bloques de roca le dan una apariencia imponente, aunque mantiene una silueta natural que se integra con el entorno de la mina. Es común encontrarlo en las salas de combate, donde aparece con frecuencia como enemigo recurrente.
* **Comportamiento:** Cuando el jugador entra en su área de detección, el gólem lo persigue y ataca.
* **Función:** Introduce al jugador en el sistema de combate básico y sirve como enemigo recurrente.
<br>

### 4.4.2. RATAS

* **Tipo:** Enemigo a distancia.
* **Trasfondo:** Estas ratas han sido corrompidas por la energía cristalina que emana de las profundidades de la mina. Su cuerpo está parcialmente cubierto por fragmentos de cristal que han alterado su comportamiento y les han otorgado habilidades ofensivas. Antes eran simples animales, pero ahora defienden la mina con una agresividad inusual, como si estuvieran conectadas a su núcleo.
* **Características:** De tamaño pequeño y movimientos ágiles, las ratas tienen baja resistencia pero compensan con ataques a distancia. Los cristales incrustados en su cuerpo les permiten lanzar proyectiles afilados que dañan al jugador desde lejos. Su aspecto es inquietante: pelaje desgastado, ojos brillantes y zonas del cuerpo cubiertas por cristales que emiten un leve resplandor.
* **Comportamiento:** Las ratas vagan por la sala hasta que el jugador entra en su área de detección. En ese momento, se detienen y comienzan a disparar cristales desde la distancia. Si el jugador abandona su área, dejan de atacar y vuelven a su patrón de movimiento errático.
* **Función:** Introducen el combate a distancia y obligan al jugador a moverse, esquivar y priorizar objetivos. Su presencia en salas combinadas con enemigos cuerpo a cuerpo añade variedad táctica y refuerza la necesidad de gestionar el espacio.
<br>

### 4.4.3. BARRILES

* **Tipo:** Enemigo Kamikaze.
* **Trasfondo:** Estos conejos han encontrado una forma peculiar de defender la mina: se esconden dentro de barriles reforzados y se lanzan contra los intrusos. Se desconoce si actúan por voluntad propia o si han sido manipulados por la corrupción que afecta a la mina. Lo que está claro es que su método es tan absurdo como efectivo.
* **Características:** De tamaño medio, los barriles explosivos se mueven rápidamente en línea recta hacia el jugador. Al impactar o al acercarse lo suficiente, detonan causando daño en área. Su diseño es cómico pero peligroso: un barril con patas, orejas asomando por la tapa y una mecha encendida que indica su inminente explosión.
* **Comportamiento:** Al detectar al jugador, corren directamente hacia él sin detenerse. No atacan de forma convencional, sino que explotan al contacto o tras unos segundos. Si el jugador logra esquivarlos, pueden explotar sin causar daño, pero en espacios cerrados su amenaza aumenta.
* **Función:** Sirven para añadir tensión y dinamismo en combate. Obligan al jugador a moverse constantemente y a estar atento al entorno.
<br>

## 4.5. ARMAS Y ARMADURAS

La progresión del jugador se basa en la mejora del equipo mediante los materiales recolectados en la mina. Existen tres tipos de elementos equipables: arma (pico), vestimenta (torso) y casco, cada uno con efectos específicos sobre el rendimiento del jugador. Las mejoras se realizan en el pueblo, a través de la herrería y la tienda de ropa.

### 4.5.1. MEJORA DE ARMA - PICO

El pico es el arma principal del jugador. Se mejora en la herrería, utilizando materiales específicos. Cada nivel incrementa tanto el daño y la velocidad de ataque, como la probabilidad y el daño de los ataques críticos. 

| Nivel | Nombre | Efecto | Precio de mejora |
| :---: | :----: | :----: | :----: |
| 1 | Pico oxidado | 10 puntos de daño básico, x1.25 de daño crítico, 5% de probabilidad de crítico y 0% de velocidad de ataque extra | Arma inicial |
| 2 | Pico reforzado | 12 puntos de daño básico, x2 de daño crítico, 6% de probabilidad de crítico y 5% de velocidad de ataque extra | 70 monedas y 15 lingotes de hierro |
| 3 | Pico afilado | 15 puntos de daño básico, x2.5 de daño crítico, 8% de probabilidad de crítico y 10% de velocidad de ataque extra | 100 monedas, 25 lingotes de hierro y 5 piedras de carbón |
| 4 | Pico endurecido | 20 puntos de daño básico, x3 de daño crítico, 10% de probabilidad de crítico y 15% de velocidad de ataque extra | 200 monedas, 40 lingotes, 10 piedras de carbón y 5 piedras de obsidiana |
| 5 | Pico maestro | 30 puntos de daño básico, x5 de daño crítico, 15% de probabilidad de crítico y 20% de velocidad de ataque extra | 300 monedas, 45 lingotes de hierro, 15 piedras de carbón y 15 piedras de obsidiana |

<div align="center"><p><i>Tabla 3: Mejoras de armas.</i></p></div>

<br>

### 4.5.2. VESTUARIO - PRENDAS DEL TOPO

Las prendas modifican la velocidad de movimiento, velocidad de ataque y la defensa del jugador. Se adquieren en la tienda de ropa, utilizando rollos de tela y lingotes de bronce.

| Nombre | Efecto | Precio |
| :---: | :----: | :----: |
| Chaleco reforzado | 100 puntos de vida, +0% velocidad y +0% velocidad de ataque | Prenda inicial |
| Túnica de minero | 60 puntos de vida, +30% velocidad y +30% velocidad de ataque | 300 monedas, 30 rollos de tela y 20 lingotes de bronce |
| Armadura pesada | 170 puntos de vida, -30% velocidad y -30% velocidad de ataque | 300 monedas, 10 rollos de tela y 60 lingotes de bronce | 

<div align="center"><p><i>Tabla 4: Prendas del torso.</i></p></div>

<br>
  
### 4.5.3. VESTUARIO - CASCOS

Los cascos afectan la defensa, la velocidad y la velocidad de ataque. Se fabrican en la tienda de ropa, utilizando lingotes de bronce y cristales de cuarzo.

| Nombre | Efecto | Precio |
| :---: | :----: | :----: |
| Casco de minero | 50 puntos de vida, +0% velocidad y +0% velocidad de ataque | Casco inicial |
| Casco reforzado | 70 puntos de vida, +10% velocidad y +10% velocidad de ataque | 200 monedas, 10 lingotes de bronce y 5 piedras de cuerzo |
| Casco blindado | 120 puntos de vida, -20% velocidad y +30% velocidad de ataque | 800 monedas, 50 lingotes de bronce y 15 piedras de cuarzo |
| Casco ligero | 50 puntos de vida, +30% velocidad y +50% velocidad de ataque | 500 monedas, 30 lingotes de bronce y 10 piedras de cuarzo |

<div align="center"><p><i>Tabla 5: Cascos.</i></p></div>

<br>
  
### 4.5.4. MATERIALES RECOGIBLES

Durante la exploración de la mina, el jugador puede recolectar materiales que se dividen en dos categorías:

**Materiales de venta:** Valiosos por su rareza o belleza, se venden en la tienda de ropa para obtener monedas.

| Nombre | Rareza | Beneficio |
| :---: | :----: | :----: |
| Ámbar | Común | 8 monedas |
| Esmeralda | Común | 50 monedas |
| Rubí | Raro | 70 monedas |
| Diamante | Muy raro | 100 monedas |

<div align="center"><p><i>Tabla 6: Materiales para la venta.</i></p></div>

<br>

**Materiales de mejora:** Utilizados para mejorar el equipo, aunque también pueden venderse.

| Nombre | Utilidad | Rareza | Beneficio |
| :---: | :----: | :----: | :-------: |
| Lingotes de hierro | Mejora de armas | Común | 4 monedas |
| Carbón | Mejora de armas | Raro | 15 monedas |
| Lingotes de bronce | Fabricación de armaduras y cascos | Raro | 25 monedas |
| Cristales de cuarzo | Fabricación de cascos | Muy raro | 50 monedas |
| Rollos de tela | Fabricación de prendas | Muy raro | 50 monedas |
| Cristales de obsidiana | Mejora de armas | Muy raro | 50 monedas |

<div align="center"><p><i>Tabla 7: Materiales para mejorar.</i></p></div>

<br>

## 4.6. NIVELES, EVENTOS Y MISIONES
### 4.6.1. NIVELES DE PROFUNDIDAD

La mina está dividida en niveles progresivos, cada uno con su propio entorno, enemigos y materiales. Cada vez que el jugador vence al jefe final de un nivel, se desbloquea el siguiente, aumentando la dificultad y la variedad de desafíos. Los niveles presentan diferencias visuales, mecánicas y estratégicas, incentivando la exploración y la mejora constante del equipo.

### 4.6.2. EVENTOS

Durante la exploración de la mina, el jugador puede encontrarse con salas especiales que aparecen de forma aleatoria, según un porcentaje de aparición predefinido. Estos eventos aportan variedad, sorpresa y decisiones tácticas que enriquecen el desarrollo de cada run.

**Sala del Tesoro:** Esta sala aparece sin enemigos. En el centro se encuentra un gran cofre que puede contener objetos raros, materiales valiosos o grandes cantidades de dinero. Su aparición es poco frecuente

**Sala Oscura:** En esta sala no hay iluminación ambiental, por lo que el jugador debe depender exclusivamente de la luz que emite su casco. Los enemigos se ocultan en las sombras, dificultando su detección y aumentando la tensión del combate.

**Sala de Rescate:** Aquí el jugador encuentra a un NPC atrapado por enemigos. Para completar el evento, debe enfrentarse a los enemigos que rodean al personaje y liberarlo. Si lo consigue, el NPC puede ofrecer una recompensa o iniciar un intercambio.

**Sala de Campamento:** Esta sala ofrece un momento de calma dentro de la mina. En ella, el jugador encuentra un pequeño campamento con una hoguera encendida. Junto a la hoguera se encuentra Pico, el canario del tutorial, que actúa como personaje de apoyo. Si el jugador habla con él, puede recuperar parte de su salud, lo que convierte esta sala en un punto estratégico para continuar la exploración.

### 4.6.3. MISIONES

Las misiones se integran dentro de los eventos, especialmente en las salas de rescate. Cuando el jugador encuentra un NPC atrapado, se activa la misión de rescatarlo de los enemigos. Si tiene éxito, el NPC puede ofrecer una recompensa directa, un intercambio de objetos o desbloquear contenido adicional. Estas misiones aportan variedad narrativa y recompensas únicas dentro de la estructura de las runs.

<br>
  
# 5. INTERFAZ  
## 5.1. DIAGRAMA DE FLUJO

A continuación se muestra el diagrama de flujo de las interfaces del juego, donde se representa cómo se conectan entre sí las distintas pantallas y menús mediante el uso de botones. Las pantallas de herrería, tienda y armario se abren cuando el jugador viaja a esos sitios dentro de la aldea y pulsa el botón de interactuar. 

![Imagen del diagrama de flujo de las interfaces](/Fotos_README/Flujo_de_interfaces.png "Diagrama de flujo de las interfaces")  
<div align="center"><p><i>Imagen 1: Diagrama de flujo de las interfaces</i></p></div>  

<br>

## 5.2. PANTALLAS EN EL MENÚ PRINCIPAL  

![Imagen del menú inicial](/Fotos_README/menuInicial.png "Menú inicial")  
<div align="center"><p><i>Imagen 2: Pantalla principal del menú inicial.</i></p></div>  

<br>

![Imagen de los créditos](/Fotos_README/creditos.png "Pantalla de créditos")  
<div align="center"><p><i>Imagen 3: Pantalla de créditos, con scroll vertical.</i></p></div>  

<br>

![Imagen de los ajustes](/Fotos_README/ajustes.png "Pantalla de ajustes desde el menú principal")  
<div align="center"><p><i>Imagen 4: Ajustes de sonido dentro del menú principal.</i></p></div>  

<br>

## 5.3. PANTALLAS DE PAUSA Y AJUSTES EN PARTIDA

![Imagen del menú de pausa](/Fotos_README/pausa.png "Menú de pausa")  
<div align="center"><p><i>Imagen 5: Pantalla de pausa en partida.</i></p></div>  

<br>

![Imagen de los ajustes](/Fotos_README/ajustesPartida.png "Pantalla de ajustes desde la partida")  
<div align="center"><p><i>Imagen 6: Ajustes de sonido dentro de partida.</i></p></div>  

<br>

## 5.4. INTERFAZ PRINCIPAL EN PARTIDA Y EN LA ALDEA  

La pantalla principal sirve para abrir el baúl dentro de la villa o la mochila desde dentro de las minas, aunque también se puede abrir con el teclado o el mando. Dentro de las minas se le muestra al jugador sus puntos de vida, mediante una barra luminosa.  

![Imagen de la interfaz en la mina](/Fotos_README/interfazMina.png "Interfaz principal en la mina")  
<div align="center"><p><i>Imagen 7: Interfaz principal en la mina.</i></p></div>  

<br>

![Imagen de la interfaz en la aldea](/Fotos_README/interfazAldea.png "Interfaz principal en la aldea")  
<div align="center"><p><i>Imagen 8: Interfaz principal en la aldea.</i></p></div>  

<br>

## 5.5. PANTALLA DE BAÚL Y DE MOCHILA  

Mientras el usuario está en la mina puede ver que materiales ha conseguido en ella y cierta información relevante. Cuando llegue a la aldea se le guardarán parte de los materiales de la mochila dentro del baúl, y podrá verlos todos del mismo modo.  

![Imagen de la interfaz de la mochila en la mina](/Fotos_README/interfazMochilaMina.png "Interfaz de la mochila en la mina")  
<div align="center"><p><i>Imagen 9: Interfaz de mochila en las minas.</i></p></div>  

<br>

![Imagen de la interfaz del baúl en la aldea](/Fotos_README/interfazBaulAldea.png "Interfaz de baúl en la aldea")  
<div align="center"><p><i>Imagen 10: Interfaz de baúl en la aldea.</i></p></div>  

<br>

## 5.6. PANTALLA DE VENTA  

Se muestra cuando el usuario viaja a la tienda dentro de la aldea e interacciona con el tendero. En ella se le permite seleccionar materiales de su baúl y venderlos.  

![Imagen de la interfaz de venta](/Fotos_README/interfazVenta.png "Interfaz de venta")  
<div align="center"><p><i>Imagen 11: Interfaz de venta.</i></p></div>  

<br>  

## 5.7. PANTALLA DE HERRERÍA  

Cuando el usuario viaja a la herrería dentro de la aldea se le permite interactuar con el herrero para gastar sus materiales en la mejora del pico.  

![Imagen de la interfaz de herrería](/Fotos_README/interfazHerreria.png "Interfaz de herrería")  
<div align="center"><p><i>Imagen 12: Interfaz de herrería.</i></p></div>  

<br>  

## 5.8. PANTALLA DE ARMARIO  

Dentro de la tienda de venta, el usuario también puede abrir un armario para comprar y equipar diferentes prendas como cascos y petos.  

![Imagen de la interfaz de armario](/Fotos_README/pantallaArmario.png "Interfaz de armario")  
<div align="center"><p><i>Imagen 13: Interfaz de armario.</i></p></div>  

<br>    

## 5.9. PANTALLA DE DERROTA  

Cuando el jugador muere o completa una run, se le muestra un menú de derrota con los materiales conseguidos y un botón para volver a la aldea.  

![Imagen de la interfaz de fin de partida](/Fotos_README/interfazDerrota.png "Interfaz de fin de partida")  
<div align="center"><p><i>Imagen 14: Interfaz de fin de partida.</i></p></div>  

<br> 

---

<br>

# 6. MÚSICA Y EFECTOS DE SONIDO

El apartado sonoro busca reforzar la atmósfera del juego y acompañar la experiencia del jugador. La música y los efectos de sonido están diseñados para ser coherentes con la estética visual y narrativa del juego. 

## 6.1. MÚSICA   

* Música ambiental para el pueblo:
     * Audio original: [Easy Winner](https://youtu.be/NdCBT_VHnUk)
     * Modificaciones: Recorte y reducción de velocidad y tono al 80%.
     * Audio original: [Pavane for a Dead Princess](https://youtu.be/q9tcHoD6r0c)
     * Modificaciones: Recorte y reducción de velocidad al 95%.
* Música ambiental para la mina:
     * Audio original: [Arebesque No. 1](https://youtu.be/cVYH-7QGE-A) y [Debussy](https://youtu.be/_CUC2-S1NMI)
     * Modificaciones: Recorte, reducción de velocidad y tono al 95% y eco.
* Música para la tienda:
     * Audio original: [Je te veux](https://youtu.be/wbT9DeULzU4)
     * Modificaciones: Recorte y reducción de velocidad y tono al 80%.
* Música para combate contra enemigos:
     * Audio original: [Spanish Folk Songs](https://youtu.be/9OT9bQdU-SI)
     * Modificaciones: Recorte y aumento de velocidad al 200%.

## 6.2. EFECTOS DE SONIDO  

* Sonido de jugador caminando:
     * Audio original: [Sonido caminar](https://youtube.com/shorts/S64xVwJrmys?si=oVvlcTmIRvenMPw6)
     * Modificaciones: Recorte y reducción de tono al 70%.
* Sonido de jugador atacando:
     * Audio original: [Sonido ataque](https://youtube.com/shorts/S64xVwJrmys?si=oVvlcTmIRvenMPw6)
     * Modificaciones: Recorte y aumento de tono al 120%.
* Sonido de enemigo muerto:
     * Audio original: [Sonido muerte enemigo](https://youtube.com/shorts/S64xVwJrmys?si=oVvlcTmIRvenMPw6)
     * Modificaciones:  Recorte y reducción de tono al 80%.
* Sonido de hoguera:
     * Audio original: [Sonido hoguera](https://www.youtube.com/watch?v=GdwhlKKw0Lc&pp=ygUKZmlyZSBzb3VuZA%3D%3D)
     * Modificaciones: Recorte y reducción de la velocidad al 80%.
<br>

---

<br>

# 7. EQUIPO

**Lara Sánchez Sanz:** Modelado y animación 3D de personajes.  
**Jesús Mercado Rioja:** Diseño 2D y modelado 3D de escenarios.  
**Ana María Caamaño del Amo:** Modelado 3D de props.  
**Javier Martín Mulero:** Programación de mecánicas principales.  
**Unai Pastrana Torres:** Diseño y programación de interfaces.  
**Santiago Varela Rey:** Música y efectos de sonido.

<br>

---

<br>

# 8. POSTMORTEM
## 8.1. INDIVIDUAL
### 8.1.1. QUÉ SALIÓ BIEN

**Ana María Caamaño del Amo:** se ha conseguido entregar una beta con la mayoría de las salas que se habían pensado, además de tener un aspecto visual más cercano a lo deseado. Respecto al apartado artístico, se han implementado y modelado muchos elementos de forma bastante más rápida que en la versión anterior.

**Javier Martín Mulero:** el proyecto ha ido medianamente bien, por mi parte, la programación la dejé casi acabada al final de la beta, lo que me ha permitido arreglar bugs y ayudar a Unai con su parte de programación.

**Lara Sánchez Sanz:** durante esta entrega he conseguido completar y mejorar el modelado y la animación de los personajes principales y enemigos. El flujo de trabajo ha sido más ágil que en la versión anterior, lo que ha permitido integrar los modelos y animación en Unity sin grandes problemas. Estoy realmente satisfecha con la variedad de personajes y animaciones implementadas. 

**Jesús Mercado Rioja:** el proyecto ha ido avanzando correctamente en base a los objetivos que se pusieron al inicio de la fase y se ha logrado evolucionar el juego en el  tiempo estimado, tanto el aspecto artístico del juego como el técnico ha avanzado con la inclusión de nuevos personajes, escenarios y props.

**Unai Pastrana Torres:** Estoy orgulloso con el desempeño de la beta en cuanto al trabajo que me tocó implementar, ya que, conseguí hacer un diseño de interfaz agradable e intuitivo, que recogía todos los menús que planifiqué hacer en esta entrega.

**Santiago Varela Rey:** durante este sprint he ampliado notablemente el apartado sonoro del proyecto. Se han añadido nuevos efectos de sonido y piezas musicales adaptadas a cada situación del juego (combate, muerte, caminar, clickear, música para escenarios, etc.). Además, toda esta nueva implementación se integró correctamente en Unity, asegurando que cada sonido se active en el momento adecuado mediante eventos y triggers. La comunicación con el equipo fue fluida durante el proceso de integración, lo que permitió ajustar los sonidos de forma coherente con la estética cartoon del juego.



### 8.1.2. QUÉ SALIÓ MAL

**Ana María Caamaño del Amo:** en la última entrega se comenzó usando Trello y cuanto más avanzaba el desarrollo su uso descendió, en esta entrega ha sido peor; ya que ni siquiera se ha utilizado en absoluto y se ha favorecido la comunicación directa. Otra cosa serían las redes sociales, las cuales han estado completamente inactivas durante la gran mayoría del desarrollo. 

**Javier Martín Mulero:** en esta entrega la comunicación con algunos de los miembros del equipo no ha sido tan buena, por no decir mala. Además, hay cosas que se han quedado fuera, como un tipo de evento (rescate) por falta de tiempo, la lógica de las salas estaba hecha faltaban los detalles como el modelo del NPC a rescatar y su interacción. Por último, las redes sociales no se actualizaron.

**Lara Sánchez Sanz:** el principal problema ha sido la gestión del tiempo. Al tener que compaginar este trabajo con otras asignaturas lo que no ha permitido desarrollar el juego al nivel que teníamos esperado. Además la falta de herramientas de organización ha dificultado la coordinación con los compañeros, lo que provocó que algunas integraciones se hicieran más tarde de lo previsto.

**Jesús Mercado Rioja:** a pesar de que se planteó en la fase alfa hacer un buen uso de la herramienta Trello para comunicarnos, no se puso en práctica, y de hecho empeoró respecto a la anterior entrega, ya que cayó prácticamente en desuso por lo que a pesar de que nos hemos comunicado entre nosotros de forma grupal, no existía un registro exacto de que estaba hecho y que no, lo que dificultaba el proceso de trabajo. Además, no comenzamos el desarrollo de la beta de la manera más inmediata, forzándonos así a que la carga de trabajo fuera más alta en un menor tiempo.

**Unai Pastrana Torres:** a la hora de llevar a cabo este trabajo al mismo tiempo que realizábamos proyectos de otras asignaturas ha supuesto tener que hacer el trabajo muy rápido sin apenas tiempo para testear adecuadamente el código.

A la hora de implementar ciertas funcionalidades de la UI que dependían del desarrollo de las animaciones, del modelado del vestuario, o de la programación de la música, tuve el problema de tener que esperar a que se terminaran, lo cual no me dejó el suficiente tiempo para implementarlas desde código de la forma que hubiera querido.

**Santiago Varela Rey:** a pesar de que se completaron las tareas de sonido y música, todavía hubo momentos en los que la planificación podría haber sido más efectiva. Algunas integraciones se dejaron para etapas tardías del desarrollo y han requerido de la ayuda de otros miembros del equipo que estaban disponibles. Además, una de las piezas musicales tuvo que ser reemplazada por incumplimiento del copyright en los días previos a la entrega.

### 8.1.3. QUÉ SE PUEDE MEJORAR

**Ana María Caamaño del Amo:** algo a mejorar sería la comunicación, algo que ya fue mencionado en la anterior entrega. Habría que utilizar las herramientas proporcionadas para asegurar que todo el mundo tenga claro el estado del proyecto de forma más obvia. Otra cosa a mejorar sería la gestión del tiempo, ya que debido a unos factores externos se ha empezado esta fase de desarrollo bastante más tarde de lo que se debería y el resultado podría haber quedado incluso mejor.

**Javier Martín Mulero:** haría que la comunicación fuera diferente, ya lo dije en el anterior pero lo vuelvo a recalcar. Por lo demás, no me parece que haya que mejorar nada más. La comunicación con Unai ha sido buena para integrar el código, de otra forma se hubiera tardado mucho más.

**Lara Sánchez Sanz:** sería recomendable organizar mejor los elementos que afectan a varias áreas de trabajo,  de modo que elementos como las animaciones estén listos con antelación para facilitar el trabajo en otros apartados. También considero importante reforzar la comunicación y el uso de herramientas colaborativas, ya que con respecto de la entrega alfa esto ha empeorado. A nivel técnico, me gustaría dedicar más tiempo al pulido de las animaciones y modelados.

**Jesús Mercado Rioja:** se debería de gestionar mejor el tiempo de trabajo, para que así este no se nos acumule. Además deberíamos hacer un buen uso de la herramienta Trello u otras plataformas para dejar constancia de que partes del trabajo ya se han realizado y cuáles quedan por hacer y así conseguir que todo el mundo tenga claro en que tiene que trabajar. 

**Unai Pastrana Torres:** mejorar la comunicación con mis compañeros hubiese supuesto una mejora en aquellas tareas que dependen de mis compañeros, por ejemplo la programación de los ajustes de volumen, o mostrar el equipamiento equipado en la UI. Para mejorar la comunicación propondría dejar muy claro que necesita cada uno de otros compañeros, y dejar terminadas esas tareas con una mayor prioridad. 

**Santiago Varela Rey:** Para próximas entregas sería recomendable establecer un pipeline más eficiente para los archivos de audio, definiendo mejor la estructura de carpetas, nombres y buses de mezcla. Otra mejora sería terminar antes las tareas sonoras principales para disponer de más margen para la mezcla fina y el pulido final.
  

### 8.1.4. CONCLUSIÓN

**Ana María Caamaño del Amo:** a pesar de los problemas que han surgido y el comienzo tardío, se ha conseguido entregar una versión beta bastante completa con la mayoría de lo que teníamos en mente implementado.

**Javier Martín Mulero:**  la beta ha salido bien, tiene lo que tiene que tener, aunque a mi me hubiera gustado incluir todo lo que se tenía pensado.

**Lara Sánchez Sanz:** estoy contenta con el resultado obtenido en la beta, se que podría haber sido mejor, pero realmente ha salido mejor de lo que esperaba, lo que junto con la mejora en la fluidez del trabajo me motiva a perfeccionar aún más el aspecto del trabajo, incluso a crear nuevos elementos.

**Jesús Mercado Rioja:** la fase beta ha salido mejor de lo esperado, ya que se ha conseguido evolucionar el videojuego tanto técnica como visualmente con la implementación de nuevos elementos (escenarios, personajes, etc.). A pesar de los problemas que han ido surgiendo, el equipo ha sabido resolverlos y aunque se deben pulir algunos detalles, estoy orgulloso del resultado obtenido.

**Unai Pastrana Torres:** en resumen estoy orgulloso del desempeño general de equipo en esta entrega, y de los resultados dentro del juego. Considero que la beta está bastante completa, aunque dejáramos ciertos detalles sin pulir, tenemos una base sólida sobre la que trabajar.

**Santiago Varela Rey:** estoy satisfecho con la ampliación y mejora del apartado sonoro en esta entrega. La implementación de nuevos efectos y música ha contribuido a hacer el juego más coherente y con mayor personalidad audiovisual. Aunque la planificación podría haberse optimizado, el resultado final es positivo y sirve como una base sólida para seguir refinando el audio.
  

## 8.2. GRUPAL
### 8.2.1. QUÉ SALIÓ BIEN

La fase beta ha supuesto un avance significativo para el proyecto, logrando entregar una versión más completa y cercana a lo que se había planteado inicialmente. Se han cumplido los plazos y se han implementado mejoras notables en todos los apartados. El resultado ha superado en muchos aspectos lo esperado, consolidando una base sólida sobre la que seguir trabajando.  

### 8.2.2. QUÉ SALIÓ MAL

Los principales problemas han estado relacionados con la gestión del tiempo y la comunicación interna. El uso de herramientas colaborativas como Trello se abandonó progresivamente, lo que dificultó la coordinación y provocó retrasos en la integración del trabajo en Unity. Además, la carga de trabajo simultánea con otras asignaturas redujo la cantidad de tiempo para pulir errores y realizar más pruebas.


### 8.2.3. QUÉ SE PUEDE MEJORAR

De cara al Gold Release, en el equipo coincidimos en la necesidad de reforzar la comunicación y la organización, estableciendo un flujo de trabajo más eficiente y asegurando que las tareas estén con antelación para facilitar la integración en Unity.

A nivel técnico, estaría bien dedicar más tiempo a ampliar y dar variedad tanto a los escenarios como los personajes, así como mejorar ciertos aspectos de la música y la programación.   

### 8.2.4. CONCLUSIÓN

En conclusión, el equipo está satisfecho con el resultado de la beta. A pesar de las dificultades, se ha conseguido evolucionar el juego tanto técnica como visualmente, y la motivación para la siguiente fase es alta. La experiencia adquirida para esta entrega servirá para optimizar procesos y asegurar que la versión final alcance un nivel aún más pulido y completo.

<br>
<br>

# ANEXOS  
## ANEXO 1: TURNAROUND DE OTTO  

En este anexo se presentan las vistas en 2D y 3D del personaje Otto, utilizadas para el modelado y animación.  

![Imagen del turnaround de Otto](/Fotos_README/turnaroundOtto.png "Turnaround de Otto")  
<div align="center"><p><i>Imagen A1.1: Turnaround de Otto en 2D.</i></p></div>  

<br><br> 

![Imagen del turnaround de Otto 3D](/Fotos_README/turnaroundOtto3D.png "Turnaround de Otto 3D")  
<div align="center"><p><i>Imagen A1.2: Turnaround de Otto en 3D.</i></p></div>  

<br>

## ANEXO 2: BEAUTY DE OTTO   

En este anexo se incluye el beauty render del personaje Otto, mostrando su aspecto final tras el proceso de modelado, texturizado y animación.  

<div align="center"><img src="/Fotos_README/beautyOtto.png" alt="Beauty de Otto" width="400"></img></div>  
<div align="center"><p><i>Imagen A2.1: Beauty de Otto en 2D.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/beautyOtto3D.png" alt="Beauty de Otto en 3D" width="400"></img></div>  
<div align="center"><p><i>Imagen A2.2: Beauty de Otto en 3D.</i></p></div>  
<br>

## ANEXO 3: ARMAS Y ARMADURAS  

En este anexo se recopilan los diseños finales de las armas y armaduras disponibles en Rock & Mole.   

![Imagen de las armaduras](/Fotos_README/armaduras.png "Armaduras")  
<div align="center"><p><i>Imagen A3.1: Armaduras.</i></p></div>  

<br><br>

![Imagen de los cascos](/Fotos_README/cascos.png "Cascos")  
<div align="center"><p><i>Imagen A3.2: Cascos.</i></p></div>  

<br><br> 

![Imagen de los picos](/Fotos_README/picos.png "Picos")  
<div align="center"><p><i>Imagen A3.3: Picos.</i></p></div>  

<br>

## ANEXO 4: TURNAROUND DE LOS ENEMIGOS  

En este anexo se presentan los turnarounds en 2D y 3D de los enemigos principales del juego, mostrando sus vistas completas para el modelado y la animación.  

![Imagen del turnaround del gólem grande](/Fotos_README/turnaroundGolemGrande.png "Turnaround del golem grande")  
<div align="center"><p><i>Imagen A4.1: Turnaround del gólem grande en 2D.</i></p></div>  

<br><br> 

![Imagen del turnaround del gólem grande en 3D](/Fotos_README/turnaroundGolemGrande3D.png "Turnaround del golem grande en 3D")  
<div align="center"><p><i>Imagen A4.2: Turnaround del gólem grande en 3D.</i></p></div>  

<br><br>

![Imagen del turnaround del gólem pequeño](/Fotos_README/turnaroundGolemPequeno.png "Turnaround del gólem pequeño")  
<div align="center"><p><i>Imagen A4.3: Turnaround del gólem pequeño en 2D.</i></p></div>  

<br><br> 

![Imagen del turnaround del gólem pequeño en 3D](/Fotos_README/turnaroundGolemPequeno3D.png "Turnaround del gólem pequeño en 3D")  
<div align="center"><p><i>Imagen A4.4: Turnaround del gólem pequeño en 3D.</i></p></div>  

<br><br>

![Imagen del turnaround del conejo](/Fotos_README/turnaroundConejo.png "Turnaround del conejo")  
<div align="center"><p><i>Imagen A4.5: Turnaround del conejo en 2D.</i></p></div>  

<br><br>

![Imagen del turnaround de la rata](/Fotos_README/turnaroundRata.png "Turnaround de la rata")  
<div align="center"><p><i>Imagen A4.6: Turnaround de la rata en 2D.</i></p></div>  

<br>

## ANEXO 5: BEAUTY DE LOS ENEMIGOS  

En este anexo se presentan los beauty finales de los enemigos del juego.  

<div align="center"><img src="/Fotos_README/beautyGolem.png" alt="Beauty del gólem" width="400"></img></div>  
<div align="center"><p><i>Imagen A5.1: Beauty del gólem grande en 2D.</i></p></div>  
<br>

## ANEXO 6: TURNAROUND DE LOS NPCS  

En este anexo se presentan los turnarounds en 2D de los NPCs principales del juego, mostrando todas sus vistas completas para modelado y animación.

![Imagen del turnaround del herrero](/Fotos_README/turnaroundHerrero.png "Turnaround del herrero")  
<div align="center"><p><i>Imagen A6.1: Turnaround del tendero y el herrero en 2D.</i></p></div>  

<br><br>

![Imagen del turnaround del pájaro](/Fotos_README/turnaroundPajaro.png "Turnaround del pájaro")  
<div align="center"><p><i>Imagen A6.2: Turnaround del pájaro en 2D.</i></p></div>  

<br>

## ANEXO 7: CONCEPTS DE ESCENARIOS  

En este anexo se incluyen los concepts arts de los escenarios del juego, que muestran las ideas visuales y propuestas para los distintos ambientes.  

<div align="center"><img src="/Fotos_README/conceptAldea.png" alt="Concept del pueblo" width="500"></img></div>  
<div align="center"><p><i>Imagen A7.1: Concept art del pueblo.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/conceptHerreria.png" alt="Concept de la herrería" width="500"></img></div>  
<div align="center"><p><i>Imagen A7.2: Concept art de la herrería.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/conceptMina.png" alt="Concept de la mina" width="500"></img></div>  
<div align="center"><p><i>Imagen A7.3: Concept art de la mina.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/conceptArmario.png" alt="Concept de la tienda de ropa" width="500"></img></div>  
<div align="center"><p><i>Imagen A7.4: Concept art de la tienda de ropa.</i></p></div>  
<br>

## ANEXO 8: PROPS   

En este anexo se recopilan los props diseñados para los escenarios del juego, elementos secundarios que enriquecen la ambientación y aportan coherencia visual al entorno.  

<div align="center"><img src="/Fotos_README/cofre.png" alt="Cofre" width="500"></img></div>  
<div align="center"><p><i>Imagen A8.1: Cofre.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/jaula.png" alt="Jaula" width="500"></img></div>  
<div align="center"><p><i>Imagen A8.2: Jaula.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/roca1.png" alt="Diseño de un tipo de roca" width="500"></img></div>  
<div align="center"><img src="/Fotos_README/roca2.png" alt="Diseño de un segundo tipo de roca" width="500"></img></div>  
<div align="center"><img src="/Fotos_README/roca3.png" alt="Diseño de tercer tipo de roca" width="500"></img></div>  
<div align="center"><p><i>Imagen A8.3: Rocas.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/pala.png" alt="Pala" width="300"></img></div>  
<div align="center"><p><i>Imagen A8.4: Pala.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/barril.png" alt="Barril" width="500"></img></div>  
<div align="center"><p><i>Imagen A8.5: Barril.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/cajas.png" alt="Cajas" width="500"></img></div>  
<div align="center"><p><i>Imagen A8.6: Cajas.</i></p></div>  
<br><br>

<div align="center"><img src="/Fotos_README/vagon.png" alt="Vagón" width="500"></img></div>  
<div align="center"><p><i>Imagen A8.7: Vagón.</i></p></div>  
<br><br>














