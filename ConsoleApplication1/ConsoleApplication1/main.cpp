#include <iostream>

using namespace std;

class UniqueObject 
{ 
private: 
// Constructeur/destructeur 
UniqueObject () : _value (0) { } 
~UniqueObject () { }
 public: 
  // Interface publique 
  void setValue (int val)    { _value = val; }
    int getValue () { return _value; } 
   // Fonctions de création et destruction du singleton 
   static UniqueObject *getInstance ()
    {
      if (NULL == _singleton)   { 
                                        std::cout << "creating singleton." << std::endl; 
                                        _singleton = new UniqueObject; 
                                                  } else  
		 { 
                             std::cout << "singleton already created!" << std::endl; 
                                                      } 
        return _singleton; 
     } 
  static void kill () 
         { 
           if (NULL != _singleton)  
             { 
                   delete _singleton; _singleton = NULL; 
              } 
        } 
 private: // Variables membres 
 int _value; 
static UniqueObject *_singleton; 
}; 
// Initialisation du singleton à NULL 
UniqueObject *UniqueObject::_singleton = NULL; 


int main () 
{ 
// pointeurs sur l'unique instance de la classe UniqueObject 

UniqueObject *obj1, *obj2; 
// initialisation des pointeurs 

obj1 = UniqueObject::getInstance (); 
obj2 = UniqueObject::getInstance (); 

// affectation de la valeur 11 à l'objet pointé par obj1 

obj1->setValue (11); 

// affichage de _value 

std::cout << "obj1::_value = " << obj1->getValue () << std::endl; 
std::cout << "obj2::_value = " << obj2->getValue () << std::endl; 

// destruction de l'instance unique 

obj1->kill (); return 0; 
} 
