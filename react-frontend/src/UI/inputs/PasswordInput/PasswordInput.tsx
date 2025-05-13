import { useState } from 'react';
import TextInput from '../TextInput/TextInput';
import { FaEye, FaEyeLowVision } from 'react-icons/fa6';

interface PasswordInputProps {
    password: string,
    setPassword: (newValue: string) => void,
    className?: string
}

const PasswordInput = ({ password, setPassword, className }: PasswordInputProps) => {
    const [showPassword, setShowPassword] = useState<boolean>(true);

    return (
        <TextInput
            value={password}
            setValue={setPassword}
            rightIcon={
                showPassword ?
                    <div onClick={() => setShowPassword(false)}>
                        <FaEyeLowVision />
                    </div>
                    :
                    <div onClick={() => setShowPassword(true)}>
                        <FaEye />
                    </div>
            }
            isHidden = {showPassword}
            label='Пароль'
            placeholder='Введите пароль'
            className={className}
        />
    );
};

export default PasswordInput;