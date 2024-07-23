import styles from "./avatar-upload.module.css";
import React, { ChangeEvent } from "react";
import { toBase64 } from "../../utils/file-utils";

interface AvatarUploadProps {
    initial: string | null, // base64 string
    onChange: (imageBase64: string) => unknown
}

export const AvatarUpload: React.FC<AvatarUploadProps> = ({ initial, onChange }) => {

    const [imageBase64, setImageBase64] = React.useState(initial);

    React.useEffect(() => {
        setImageBase64(initial);
    }, [initial]);

    const handleChange = async (e: ChangeEvent<HTMLInputElement>) => {
        const files = e.target.files;
        if (files == null) {
            return;
        }

        const file = files[0];
        if (file == null) {
            return;
        }

        const base64 = await toBase64(file);
        onChange(base64);
        setImageBase64(base64);
    }

    return (
        <label className={styles.container}>
            {imageBase64 ?
                <div className={styles.imageContainer}>
                    <img className={styles.image} src={imageBase64} alt="avatar" />
                </div>
                :
                <span className={styles.plusButton} />
            }
            <input hidden type="file" accept="image/*" onChange={handleChange} />
        </label>
    );
}